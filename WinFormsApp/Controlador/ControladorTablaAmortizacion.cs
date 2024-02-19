using System.Globalization;
using Model;
using WinFormsApp.Vista;

namespace WinFormsApp.Controlador;

public class ControladorTablaAmortizacion
{
    private readonly VistaTablaAmortizacion vista;

    public ControladorTablaAmortizacion(VistaTablaAmortizacion vista)
    {
        this.vista = vista;
        vista.closePictureBox.Click += OnCloseButtonClicked;
        vista.minimizePictureBox.Click += OnMinimizeButtonClicked;
        vista.consultarButton.Click += OnConsultarButtonClicked;
    }
    
    private void OnCloseButtonClicked(object sender, EventArgs e)
    {
        vista.Close();
    }

    private void OnMinimizeButtonClicked(object sender, EventArgs e)
    {
        vista.WindowState = FormWindowState.Minimized;
    }
    
    private async void OnConsultarButtonClicked(object sender, EventArgs e)
    {
        string nombreCliente = vista.nombreTextBox.Text;
        string monto = vista.montoTextBox.Text;
        string plazo = vista.plazoTextBox.Text;
        string porcentajeInteresAnual = vista.interesTextBox.Text;
        string? sistemaAmortizacion = vista.amortizacionComboBox.SelectedItem?.ToString();
        string? moneda = vista.monedaComboBox.SelectedItem?.ToString();
        if (nombreCliente == "" || monto == "" || plazo == "" || porcentajeInteresAnual == "" ||
            sistemaAmortizacion == "" || moneda == "")
        {
            MessageBox.Show("Por favor, llene todos los campos");
            return;
        }
        try
        {
            SistemaAmortizacion sistema = CrearSistemaAmortizacion(sistemaAmortizacion);
            Prestamo prestamo = new Prestamo(nombreCliente, double.Parse(monto), int.Parse(plazo),
                double.Parse(porcentajeInteresAnual));
            ClienteBccrWs clienteBccrWs = new ClienteBccrWs("Fauricio Granados", "fauricio.gr@gmail.com", "RRS0O0A886");
            double tipoCambioDolar = await clienteBccrWs.ObtenerTipoCambioDolar();
            double tipoCambio = moneda == "Colon" ? 1 : tipoCambioDolar;
            TablaAmortizacion tablaAmortizacion = sistema.CrearTablaAmortizacion(prestamo, tipoCambio);
            ActualizarInterfaz(tablaAmortizacion, nombreCliente, monto, plazo, porcentajeInteresAnual, tipoCambioDolar);
        }
        catch (FormatException)
        {
            MessageBox.Show("Por favor, ingrese un número válido");
        }
    }
    
    private SistemaAmortizacion CrearSistemaAmortizacion(string? sistemaAmortizacion)
    {
        switch (sistemaAmortizacion)
        {
            case "Frances":
                return new SistemaFrances();
            case "Aleman":
                return new SistemaAleman();
            case "Americano":
                return new SistemaAmericano();
            default:
                return null;
        }
    }

    private void ActualizarInterfaz(TablaAmortizacion tablaAmortizacion, string nombreCliente, string monto,
        string plazo, string porcentajeInteresAnual, double tipoCambio)
    {
        vista.RellenarTablaAmortizacion(tablaAmortizacion.ToList());
        vista.tipoCambioLabel.Text = tipoCambio.ToString(CultureInfo.InvariantCulture);
        vista.fechaLabel.Text = tablaAmortizacion.Fecha.ToString();
        vista.clienteLabel.Text = nombreCliente;
        vista.montoLabel.Text = monto;
        vista.plazoLabel.Text = plazo;
        vista.interesLabel.Text = porcentajeInteresAnual;
    }
}