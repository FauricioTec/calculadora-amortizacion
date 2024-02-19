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
            sistemaAmortizacion == "" || moneda == "" || moneda == null || sistemaAmortizacion == null)
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
            ActualizarInterfaz(tablaAmortizacion, prestamo.NombreCliente, prestamo.Monto.ToString(CultureInfo.InvariantCulture),
                prestamo.Plazo.ToString(), prestamo.PorcentajeInteresAnual.ToString(CultureInfo.InvariantCulture),
                sistemaAmortizacion, tipoCambioDolar);
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
        string plazo, string porcentajeInteresAnual, string? sistemaAmortizacion, double tipoCambio)
    {
        ActualizarDataGrid(tablaAmortizacion);
        vista.tipoCambioLabel.Text = tipoCambio.ToString(CultureInfo.InvariantCulture) + " colones";
        vista.fechaLabel.Text = tablaAmortizacion.Fecha.ToString();
        vista.clienteLabel.Text = nombreCliente;
        vista.montoLabel.Text = monto + " colones";
        vista.plazoLabel.Text = plazo;
        vista.interesLabel.Text = porcentajeInteresAnual;
        vista.sistemaAmortizacionLabel.Text = sistemaAmortizacion;
    }
    
    private void ActualizarDataGrid(TablaAmortizacion tablaAmortizacion)
    {
        vista.tablaAmortizacionDataGrid.Rows.Clear();
        
        foreach (var row in tablaAmortizacion.Detalles)
        {
            vista.tablaAmortizacionDataGrid.Rows.Add(row[0], 
                ((double)row[1]).ToString("N2"), ((double)row[2]).ToString("N2"),
                ((double)row[3]).ToString("N2"), ((double)row[4]).ToString("N2"));
        }
        
        vista.tablaAmortizacionDataGrid.Rows.Add("Total", 
            (tablaAmortizacion.TotalSaldo).ToString("N2"),
            (tablaAmortizacion.TotalIntereses).ToString("N2"), 
            (tablaAmortizacion.TotalAmortizacion).ToString("N2"),
            (tablaAmortizacion.TotalCuotas).ToString("N2"));
    }
}