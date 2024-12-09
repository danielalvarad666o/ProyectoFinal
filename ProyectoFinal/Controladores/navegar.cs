using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Controladores
{
    class navegar: ContentPage
    {
        public async void misclases(int id)
        {
            await Navigation.PushAsync(new ProyectoFinal.Views.misClases(id));

        }
    }
}
