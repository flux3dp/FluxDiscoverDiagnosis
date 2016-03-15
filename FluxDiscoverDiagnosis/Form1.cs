using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;

namespace FluxDiscoverDiagnosis
{
    struct UdpStatus
    {
        public byte[] buffer;
        public System.Net.EndPoint ipaddr;
    }

    public partial class Form1 : Form
    {
        List<DiscoverResult> dataSet;

        System.Windows.Forms.BindingSource dataResults;
        System.Timers.Timer timer;
        Socket s1;
        Socket s2;
         
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataSet = new List<DiscoverResult>();

            var results = new System. Collections.Generic.List<DiscoverResult>();
            var b = new System.ComponentModel.BindingList<DiscoverResult>(results);
            dataResults = new System.Windows.Forms.BindingSource(b, null);
            dataGridView1.DataSource = dataResults;

            timer = new System.Timers.Timer(500);
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;

            InitS2Socket();
            InitS1Socket();
        }

        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                this.Invoke(new Action(() => { dataGridView1.Refresh(); }));
            }
            catch (System.ObjectDisposedException)
            {

            }
        }

        private void LogLV1(Guid gid, System.Net.EndPoint source) 
        {
            var r = dataSet.Where(rr => rr.UUID == gid).FirstOrDefault();
            if(r != null)
            {
                r.RenewS1();
                r.EndPoint = source;
            } else
            {
                r = (DiscoverResult)dataResults.AddNew();
                r.UUID = gid;
                r.EndPoint = source;
                dataSet.Add(r);
            }
            if (r.ST2 == null || r.ST2 > 9)
            {
                var header = Encoding.ASCII.GetBytes("FLUX").Concat(new byte[] {1, 2}).Concat(gid.ToByteArray()).ToArray();
                s2.SendTo(header, source);
            }
        }

        private void LogLV2(Guid gid, string name, string ver)
        {
            var r = dataSet.Where(rr => rr.UUID == gid).FirstOrDefault();
            if (r != null)
            {
                r.RenewS2(name, ver);
            }
        }

        private void IncommingS1Message(IAsyncResult result)
        {
            if(s1 == null)
            {
                return;
            }

            System.Net.EndPoint ipaddr = new System.Net.IPEndPoint(System.Net.IPAddress.Any, 1901);
            try
            {
                s1.EndReceiveFrom(result, ref ipaddr);
            }
            catch (System.ObjectDisposedException)
            {
                return;
            }
            var st = (UdpStatus)result.AsyncState;
            int action_id = st.buffer[5];
            var gid = new Guid(st.buffer.Skip(6).Take(16).ToArray());

            if (BitConverter.ToString(st.buffer, 0, 4) == "46-4C-55-58" && action_id == 0)
            {    
                 this.Invoke(new Action<Guid, System.Net.EndPoint>(LogLV1), new object[] { gid, ipaddr });
            }

            s1.BeginReceiveFrom(st.buffer, 0, 4096, 0, ref st.ipaddr, new AsyncCallback(IncommingS1Message), st);
        }

        private void IncommingS2Message(IAsyncResult result)
        {
            System.Net.EndPoint ipaddr = new System.Net.IPEndPoint(System.Net.IPAddress.Any, 0);
            int l;
            try
            {
                l = s2.EndReceiveFrom(result, ref ipaddr);
            }
            catch (System.ObjectDisposedException)
            {
                return;
            }
            var st = (UdpStatus)result.AsyncState;
            int action_id = st.buffer[5];

            if (BitConverter.ToString(st.buffer, 0, 4) == "46-4C-55-58" && action_id == 3)
            {
                var gid = new Guid(st.buffer.Skip(6).Take(16).ToArray());
                var offset = 30;
                offset += BitConverter.ToUInt16(st.buffer, 26);
                offset += BitConverter.ToUInt16(st.buffer, 28);
                var rmsg = Encoding.UTF8.GetString(st.buffer, offset, l - offset);
                string name = "?"; string ver = "?";

                foreach(string raw in rmsg.Split('\x00'))
                {
                    if (raw.StartsWith("name=")) name = raw.Substring(5);
                    else if (raw.StartsWith("ver=")) ver = raw.Substring(4);
                    this.Invoke(new Action<Guid, string, string>(LogLV2), new object[] { gid, name, ver });
                }
            }

            s2.BeginReceiveFrom(st.buffer, 0, 4096, 0, ref st.ipaddr, new AsyncCallback(IncommingS2Message), st);
        }

        private void InitS2Socket()
        {
            s2 = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            s2.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

            UdpStatus st = new UdpStatus();
            st.buffer = new byte[4096];
            st.ipaddr = new System.Net.IPEndPoint(System.Net.IPAddress.Any, 0);

            s2.Bind(st.ipaddr);
            s2.BeginReceiveFrom(st.buffer, 0, 4096, 0, ref st.ipaddr, new AsyncCallback(IncommingS2Message), st);
        }

        private void InitS1Socket()
        {
            s1 = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            s1.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

            var mreq = System.Net.IPAddress.Parse("239.255.255.250").GetAddressBytes().Concat(System.Net.IPAddress.Any.GetAddressBytes()).ToArray();
            s1.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, mreq);
            s1.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastLoopback, true);
            s1.Bind(new System.Net.IPEndPoint(System.Net.IPAddress.Any, 1901));

            UdpStatus st = new UdpStatus();
            st.buffer = new byte[4096];
            st.ipaddr = new System.Net.IPEndPoint(System.Net.IPAddress.Any, 1901);
            s1.BeginReceiveFrom(st.buffer, 0, 4096, 0, ref st.ipaddr, new AsyncCallback(IncommingS1Message), st);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(s1 == null)
            {
                InitS1Socket();
                button1.Text = "Stop";
            } else
            {
                s1.Close();
                s1 = null;
                button1.Text = "Start";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Net.IPAddress ipaddr;
            if(!System.Net.IPAddress.TryParse(textBox1.Text, out ipaddr))
            {
                MessageBox.Show("Bad ipaddr");
                return;
            }
            var header = Encoding.ASCII.GetBytes("FLUX").Concat(new byte[] { 1, 0 }).Concat(Guid.Empty.ToByteArray()).ToArray();
            s2.SendTo(header, new System.Net.IPEndPoint(ipaddr, 1901));

        }
    }
}
