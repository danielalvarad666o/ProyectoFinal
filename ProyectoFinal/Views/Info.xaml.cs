using System.Collections.ObjectModel;
using System.Text.Json;

namespace ProyectoFinal.Views;

public partial class Info : ContentPage
{
    public ObservableCollection<ClaseDatas> Clase { get; set; } = new ObservableCollection<ClaseDatas>();

    private bool isBusy;
    public bool IsBusy
    {
        get => isBusy;
        set
        {
            isBusy = value;
            OnPropertyChanged(nameof(IsBusy));
        }
    }

    public Info(int id)
    {
        InitializeComponent();
        BindingContext = this; // Establece el contexto de datos
        LoadClase(id);
    }
    private async void LoadClase(int id)
    {
        IsBusy = true; // Mostrar el indicador de carga
        try
        {
            string apiUrl = $"http://127.0.0.1:8000/api/claseinf/{id}";
            using HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(apiUrl);

            var result = JsonSerializer.Deserialize<ApiResponses>(response);
            if (result?.success == true && result.data != null)
            {
                Clase.Clear(); // Limpiar datos anteriores
                Clase.Add(result.data); // Agregar nueva clase al ObservableCollection
                ClasesList.ItemsSource = Clase;
                var hola = "holagente";
            }
            else
            {
                await DisplayAlert("Error", "No se encontraron datos para la clase.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "No se pudieron cargar los datos de la clase: " + ex.Message, "OK");
        }
        finally
        {
            IsBusy = false; // Ocultar el indicador de carga
        }
    }
}
    public class ApiResponses
{
    public bool success { get; set; }
    public ClaseDatas data { get; set; }
}

public class ClaseDatas
{
    public int id { get; set; }
    public int clase_id { get; set; }
    public int entrenador_id { get; set; }
    public string dia_semana { get; set; }
    public string hora_inicio { get; set; }
    public string hora_fin { get; set; }
    public Clases clase { get; set; }
    public Entrenadors entrenador { get; set; }
}

public class Clases
{
    public int id { get; set; }
    public string nombre { get; set; }
    public string descripcion { get; set; }
    public int duracion_min { get; set; }
    public int max_participantes { get; set; }
}

public class Entrenadors
{
    public int id { get; set; }
    public string nombre { get; set; }
    public string apellido { get; set; }
    public string especialidad { get; set; }
    public string telefono { get; set; }
    public string correo { get; set; }
    public DateTime fecha_contratacion { get; set; }
    public int estado { get; set; }
}
