using ProyectoFinal.Controladores;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ProyectoFinal.Views
{
    public partial class misClases : ContentPage
    {
        private registrarClase _registrarClase;
        public ObservableCollection<ClaseDatainfo> Clases { get; set; }
        private bool _isBusy;

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }

        public misClases(int id)
        {
            InitializeComponent();
            _registrarClase = new registrarClase();
            Clases = new ObservableCollection<ClaseDatainfo>();
            BindingContext = this; // Establece el contexto de enlace
            CargarClases(id);
        }

        private async void CargarClases(int id)
        {
            IsBusy = true; // Mostrar indicador de carga
            try
            {
                int userId = id; // ID del usuario (puedes obtenerlo dinámicamente si es necesario)
                var clasesInscritas = await _registrarClase.ObtenerClasesInscritasAsync(userId);
                Clases.Clear(); // Limpia la colección antes de llenarla

                foreach (var clase in clasesInscritas)
                {
                    Clases.Add(clase);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"No se pudieron cargar las clases: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false; // Ocultar indicador de carga
            }
        }
    }
}
