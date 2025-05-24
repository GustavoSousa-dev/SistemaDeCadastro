using System;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace CadastroClientesWPF
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Cliente> Clientes { get; set; } = new ObservableCollection<Cliente>();

        public MainWindow()
        {
            InitializeComponent();
            ClientesDataGrid.ItemsSource = Clientes;
        }

        private void Adicionar_Click(object sender, RoutedEventArgs e)
        {
            string nome = NomeTextBox.Text;
            string email = EmailTextBox.Text;

            if (!string.IsNullOrWhiteSpace(nome) && !string.IsNullOrWhiteSpace(email))
            {
                if (IsValidEmail(email))
                {
                    Clientes.Add(new Cliente { Nome = nome, Email = email });
                    NomeTextBox.Clear();
                    EmailTextBox.Clear();
                }
                else
                {
                    MessageBox.Show("E-mail inválido. Por favor, insira um e-mail válido.");
                }
            }
            else
            {
                MessageBox.Show("Preencha todos os campos.");
            }
        }

        private void Editar_Click(object sender, RoutedEventArgs e)
        {
            if (ClientesDataGrid.SelectedItem is Cliente clienteSelecionado)
            {
                string novoEmail = EmailTextBox.Text;

                if (IsValidEmail(novoEmail))
                {
                    clienteSelecionado.Nome = NomeTextBox.Text;
                    clienteSelecionado.Email = novoEmail;
                    ClientesDataGrid.Items.Refresh();
                }
                else
                {
                    MessageBox.Show("E-mail inválido. Por favor, insira um e-mail válido.");
                }
            }
        }

        private void Excluir_Click(object sender, RoutedEventArgs e)
        {
            if (ClientesDataGrid.SelectedItem is Cliente clienteSelecionado)
            {
                Clientes.Remove(clienteSelecionado);
            }
        }

        private void ClientesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ClientesDataGrid.SelectedItem is Cliente clienteSelecionado)
            {
                NomeTextBox.Text = clienteSelecionado.Nome;
                EmailTextBox.Text = clienteSelecionado.Email;
            }
        }

        private void NomeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            NomePlaceholder.Visibility = string.IsNullOrWhiteSpace(NomeTextBox.Text)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        private void EmailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            EmailPlaceholder.Visibility = string.IsNullOrWhiteSpace(EmailTextBox.Text)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        // ✅ Função para validar e-mail
        private bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }
    }

    public class Cliente
    {
        public string Nome { get; set; }
        public string Email { get; set; }
    }
}