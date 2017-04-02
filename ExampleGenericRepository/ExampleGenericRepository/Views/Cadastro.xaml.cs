using ExampleGenericRepository.Models;
using ExampleGenericRepository.UoW;
using System;
using System.Globalization;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExampleGenericRepository.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Cadastro : ContentPage
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        private bool isEdicao;
        private Produto ProdutoEdit;
        public Cadastro(int id)
        {
            InitializeComponent();

           

            if(id > 0)
            {
                isEdicao = true;

                ProdutoEdit = _unitOfWork.ProdutoRepository.GetById(id);
                txtDescricao.Text = ProdutoEdit.Descricao;
                txtPreco.Text = ProdutoEdit.Preco.ToString();

                btnRemove.IsVisible = true;
            }
        }

        private void BtnSalvar_Clicked(object sender, EventArgs e)
        {
            if(!(string.IsNullOrEmpty(txtDescricao.Text) || string.IsNullOrEmpty(txtPreco.Text)))
            {
                //Exemplo utilizando Transaction
                //Iniciando a transação
                _unitOfWork.BeginTransaction();

                //Regra de negócio da sua aplicação....
                var produto = new Produto
                {
                    Descricao = txtDescricao.Text,
                    Preco = decimal.Parse(txtPreco.Text, CultureInfo.InvariantCulture)
                };

                if (isEdicao)
                {
                    ProdutoEdit.Descricao = txtDescricao.Text;
                    ProdutoEdit.Preco = decimal.Parse(txtPreco.Text, CultureInfo.InvariantCulture);
                    _unitOfWork.ProdutoRepository.Update(ProdutoEdit);
                }                    
                else
                    _unitOfWork.ProdutoRepository.Add(produto);

                //Commit ou Rollback de acordo com a regra
                _unitOfWork.CommitTransaction();

                Navigation.PopModalAsync();
            }
        }

        private void BtnRemover_Clicked(object sender, EventArgs e)
        {
            _unitOfWork.ProdutoRepository.Remove(ProdutoEdit.ProdutoId);

            Navigation.PopModalAsync();
        }
    }
}
