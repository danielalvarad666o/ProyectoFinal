using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Text.Json;
using ProyectoFinal.Controladores;
using ProyectoFinal.Models;

namespace ProyectoFinal.Views;

public partial class lobby : ContentPage, INotifyPropertyChanged
{
    public ObservableCollection<ClaseData> Clases { get; set; } = new ObservableCollection<ClaseData>();

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

    public lobby()
    {
        InitializeComponent();
        BindingContext = this;
        LoadClases();
    }

    private async void LoadClases()
    {
        IsBusy = true; // Mostrar el indicador de carga
        try
        {
            string apiUrl = "http://127.0.0.1:8000/api/clases";
            using HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(apiUrl);

            var result = JsonSerializer.Deserialize<ApiResponse>(response);
            if (result?.success == true)
            {
                foreach (var clase in result.data)
                {
                    Clases.Add(clase);
                }

                ClasesList.ItemsSource = Clases;
                
               
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "No se pudieron cargar las clases: " + ex.Message, "OK");
        }
        finally
        {
            IsBusy = false; // Ocultar el indicador de carga
        }
    }

    public async void Onmisclases(object sender, EventArgs e)
    {
        // Instanciar la clase 'navegar'
        var _registrarClase = new navegar();

        // Obtener el ID desde SecureStorage y convertirlo a entero
        var idString = await SecureStorage.Default.GetAsync("id");

        if (int.TryParse(idString, out int userId)) // Validamos si la conversión es exitosa
        {
            // Llamar al método misclases pasándole el ID del usuario
             _registrarClase.misclases(userId);
        }
        else
        {
            // Si la conversión falla, mostrar un mensaje de error
            await DisplayAlert("Error", "No se pudo obtener el ID del usuario.", "OK");
        }
    }

    public async void OnfavClicked(object sender, EventArgs e)
    {
        // Obtener el botón que disparó el evento
        var button = sender as Button;

        // Obtener el ID de la clase desde CommandParameter
        var id = button?.CommandParameter as int?;

        if (id == null)
        {
            await DisplayAlert("Error", "No se pudo obtener el ID de la clase", "OK");
            return;
        }

        // Navegar a la siguiente vista pasando el ID
        await Navigation.PushAsync(new ProyectoFinal.Views.Info(id.Value));
    }



    private void OnLoginClicked(object sender, EventArgs e)
    {
        // Lógica para iniciar sesión
        DisplayAlert("Login", "Iniciar sesión presionado", "OK");
        
    }
    private void OnLogoutClicked(object sender, EventArgs e)
    {
        // Lógica para cerrar sesión
        DisplayAlert("Logout", "Cerrar sesión presionado", "OK");
    }
}

public class ApiResponse
{
    public bool success { get; set; }
    public List<ClaseData> data { get; set; }
}

public class ClaseData
{
    public int id { get; set; }
    public int clase_id { get; set; }
    public int entrenador_id { get; set; }
    public string dia_semana { get; set; }
    public string hora_inicio { get; set; }
    public string hora_fin { get; set; }
    public Clase clase { get; set; }
    public Entrenador entrenador { get; set; }
}

public class Clase
{
    public int id { get; set; }
    public string nombre { get; set; }
    public string descripcion { get; set; }
    public int duracion_min { get; set; }
    public int max_participantes { get; set; }
}

public class Entrenador
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
