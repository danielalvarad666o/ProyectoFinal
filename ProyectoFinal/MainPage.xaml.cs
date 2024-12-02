using ProyectoFinal.Service;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Views;

namespace ProyectoFinal
{
    public partial class MainPage : ContentPage
    {
        private AppDbContext _dbContext;
        private ApiRouter _apiRouter;

        public MainPage()
        {
            InitializeComponent();
            _dbContext = InitializeDbContext();
            CreateDatabaseAsync().Wait();
            _apiRouter = InitializeApiRouter();
            InitializeAsync();
        }

        /// <summary>
        /// Inicializa el contexto de la base de datos SQLite.
        /// </summary>
        private AppDbContext InitializeDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlite("Filename=gym.db3");
            return new AppDbContext(optionsBuilder.Options);
        }

        /// <summary>
        /// Crea la base de datos y las tablas si no existen.
        /// </summary>
        private async Task CreateDatabaseAsync()
        {
            try
            {
                Console.WriteLine("Creando o verificando la base de datos...");
                await _dbContext.Database.EnsureCreatedAsync();
                Console.WriteLine("✅ Base de datos y tablas listas.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al crear la base de datos: {ex.Message}");
            }
        }

        /// <summary>
        /// Inicializa el cliente API.
        /// </summary>
        private ApiRouter InitializeApiRouter()
        {
            string baseUrl = "http://127.0.0.1:8000";
            return new ApiRouter(baseUrl);
        }

        /// <summary>
        /// Método para inicializar pruebas de conexión.
        /// </summary>
        private async Task InitializeAsync()
        {
            Console.WriteLine("Iniciando pruebas...");
            await TestApiAndDatabaseAsync();
        }

        /// <summary>
        /// Verifica la conexión con la API y la base de datos.
        /// </summary>
        private async Task TestApiAndDatabaseAsync()
        {
            Console.WriteLine("Verificando conexión con la API...");
            bool isApiConnected = await _apiRouter.CheckConnectionAsync();
            Console.WriteLine(isApiConnected
                ? "✅ Conexión a la API exitosa."
                : "❌ No se pudo conectar a la API. Trabajando en modo offline.");

            Console.WriteLine("Verificando conexión con SQLite...");
            try
            {
                Console.WriteLine("✅ Base de datos lista.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al conectar con la base de datos: {ex.Message}");
            }
        }

        private async void OnShowTablesClicked(object sender, EventArgs e)
        {
            try
            {
                var tables = await _dbContext.GetTablesAsync();
                string tableList = string.Join("\n", tables);
                await DisplayAlert("Tablas en la Base de Datos", tableList, "Cerrar");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"No se pudo obtener la lista de tablas: {ex.Message}", "Cerrar");
            }
        }

        private async void Onregistrar(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new ProyectoFinal.Views.Registrar());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"No se pudo navegar a la página de registro: {ex.Message}", "Cerrar");
            }
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string username = UsernameEntry.Text?.Trim();
            string password = PasswordEntry.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ErrorLabel.Text = "Por favor, ingrese usuario y contraseña.";
                ErrorLabel.IsVisible = true;
                return;
            }

            try
            {
                bool isConnected = await _apiRouter.CheckConnectionAsync();

                if (isConnected)
                {
                    bool loginSuccess = true; // Simulación del resultado de login

                    if (loginSuccess)
                    {
                        ErrorLabel.IsVisible = false;
                        Console.WriteLine("Inicio de sesión exitoso.");
                    }
                    else
                    {
                        ErrorLabel.Text = "Credenciales inválidas.";
                        ErrorLabel.IsVisible = true;
                    }
                }
                else
                {
                    ErrorLabel.Text = "No se pudo conectar al servidor.";
                    ErrorLabel.IsVisible = true;
                }
            }
            catch (Exception ex)
            {
                ErrorLabel.Text = $"Error: {ex.Message}";
                ErrorLabel.IsVisible = true;
            }
        }
    }
}
