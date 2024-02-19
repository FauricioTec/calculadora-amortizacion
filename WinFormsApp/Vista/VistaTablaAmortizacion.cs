using System.Runtime.InteropServices;

namespace WinFormsApp.Vista;

public partial class VistaTablaAmortizacion : Form
{
    //Codigo para mover el formulario sin bordes
    public const int WM_NCLBUTTONDOWN = 0xA1;
    public const int HT_CAPTION = 0x2;

    public VistaTablaAmortizacion()
    {
        InitializeComponent();
    }

    [DllImport("user32.dll")]
    public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

    [DllImport("user32.dll")]
    public static extern bool ReleaseCapture();

    // Codigo para redondear los bordes del formulario
    [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
    private static extern IntPtr CreateRoundRectRgn
    (
        int nLeftRect,
        int nTopRect,
        int nRightRect,
        int nBottomRect,
        int nWidthEllipse,
        int nHeightEllipse
    );


    private void TopPanel_MouseDown(object sender, MouseEventArgs e)
    {
        // Mover el formulario sin bordes
        if (e.Button == MouseButtons.Left)
        {
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }
    }

    private void VistaTablaAmortizacion_Load(object sender, EventArgs e)
    {
        // Redondear los bordes del formulario
        RedondearFomas();

        // Centrar los headers del datagrid
        tablaAmortizacionDataGrid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
    }

    public void RellenarTablaAmortizacion(List<List<string>> list)
    {
        tablaAmortizacionDataGrid.Rows.Clear();
        foreach (var item in list) tablaAmortizacionDataGrid.Rows.Add(item.ToArray());
    }

    // Metodo para redonder border de los elementos del formulario
    private void RedondearFomas()
    {
        // Redondear los bordes del formulario
        Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        inputPanel1.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, inputPanel1.Width, inputPanel1.Height, 5, 5));
        inputPanel2.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, inputPanel2.Width, inputPanel2.Height, 5, 5));
        inputPanel3.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, inputPanel3.Width, inputPanel3.Height, 5, 5));
        inputPanel4.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, inputPanel4.Width, inputPanel4.Height, 5, 5));
        consultarButton.Region =
            Region.FromHrgn(CreateRoundRectRgn(0, 0, consultarButton.Width, consultarButton.Height, 10, 10));
        tablaAmortizacionDataGrid.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, tablaAmortizacionDataGrid.Width,
            tablaAmortizacionDataGrid.Height, 5, 5));
    }

    private void minimizePictureBox_MouseHover(object sender, EventArgs e)
    {
        this.minimizePictureBox.BackColor = Color.FromArgb(185, 178, 170);
    }

    private void minimizePictureBox_MouseLeave(object sender, EventArgs e)
    {
        this.minimizePictureBox.BackColor = Color.FromArgb(215, 207, 200);
    }

    private void closePictureBox_MouseHover(object sender, EventArgs e)
    {
        this.closePictureBox.BackColor = Color.FromArgb(185, 178, 170);
    }

    private void closePictureBox_MouseLeave(object sender, EventArgs e)
    {
        this.closePictureBox.BackColor = Color.FromArgb(215, 207, 200);
    }
}