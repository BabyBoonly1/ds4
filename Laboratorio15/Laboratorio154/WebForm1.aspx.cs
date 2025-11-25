using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Laboratorio154
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String texto1 = TextBox1.Text;
            String texto2 = TextBox2.Text;
            int numero1;
            int numero2;
            int.TryParse(texto1, out numero1);
            int.TryParse(texto2, out numero2);
            int suma = numero1 + numero2;
            Label1.Text = $"La suma es: {suma}";
        }
    }
}