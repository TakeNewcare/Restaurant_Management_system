using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CM.Model
{
    public partial class urProduct : UserControl
    {
        public urProduct()
        {
            InitializeComponent();
        }

        public event EventHandler onSelect = null;


        public int id { get; set; }

        public string PPrice { get; set; }

        public string PCategory { get; set; }

        public string PName
        {
            get { return lblName.Text; }
            set { lblName.Text = value; }
        }

        public Image PImage
        {
            get { return txtImage.Image; }
            set { txtImage.Image = value; }
        }

        private void txtImage_Click(object sender, EventArgs e)
        {
            onSelect?.Invoke(this, e);          // 클릭 이벤트가 실행되면 onSelect 델리게이트가 호출되고, 연결된 메서드를 호출한다.(? : 연결된 메서드가 없는경우- null - 호출을 건너뛴다.)
        }
    }
}
