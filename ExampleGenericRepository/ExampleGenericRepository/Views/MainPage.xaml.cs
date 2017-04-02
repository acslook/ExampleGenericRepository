using ExampleGenericRepository.Models;
using ExampleGenericRepository.UoW;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ExampleGenericRepository.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        public ObservableCollection<Produto> ListaProdutos;
        public MainPage()
        {
            InitializeComponent();
            
            ListaProdutos = new ObservableCollection<Produto>();            

            SetItemsSourceList();
        }
        public void SetItemsSourceList()
        {
            lstViewProdutos.ItemsSource = _unitOfWork.ProdutoRepository.GetAll();
        }

        private void BtnAdicionar_ClickedAsync(object sender, EventArgs e)
        {
            var cadastro = new Cadastro(0);
            cadastro.Disappearing += Cadastro_Disappearing;

            Navigation.PushModalAsync(cadastro);
        }

        private void Cadastro_Disappearing(object sender, EventArgs e)
        {
            SetItemsSourceList();
        }       

        private void lstViewProdutos_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var cadastro = new Cadastro(((Produto)e.Item).ProdutoId);
            cadastro.Disappearing += Cadastro_Disappearing;

            Navigation.PushModalAsync(cadastro);
        }
    }
}
