using Business.Abstract.Users;
using Business.Concrete.Users;
using Business.DTOs.Users;
using DataAccess.Abstract.Users;
using DataAccess.Concrete.EntityFramework.Users;

namespace Presentation_Forms
{
    public partial class FrmRegister : Form
    {
        IUserRepository _userRepository;
        IAddressRepository _addressRepository;
        IUserService _userService;

        bool IsLeft = true;
        public FrmRegister()
        {
            InitializeComponent();

            // ------------------------------------------------------------------
            // 1. ADIM: Timer'ın Tick olayını KOD İLE manuel olarak bağlama
            // ------------------------------------------------------------------
            this.timerBackMove.Tick += new System.EventHandler(this.timerHareket_Tick);
            // ------------------------------------------------------------------

            _userRepository = new EfUserRepository();
            _addressRepository = new EfAddressRepository();
            _userService = new UserManagement(_userRepository, new SecurityHelper(), _addressRepository);
        }
        private void timerHareket_Tick(object sender, EventArgs e)
        {
            int hareketMiktari = 30;

            if (!IsLeft) // Sağa gidiyorsa
            {
                pictBack.Left += hareketMiktari;

                // Sağ sınıra ulaştıysa (Hareket bitti)
                if (pictBack.Right >= (this.ClientSize.Width * 3 / 2))
                {
                    timerBackMove.Stop();
                    // Resim sağdayken bir sonraki hareketin SOLA olması için yönü hazırla
                    IsLeft = true;
                }
            }
            else // Sola gidiyorsa
            {
                pictBack.Left -= hareketMiktari;

                // Sol sınıra ulaştıysa (Hareket bitti)
                if (pictBack.Left <= -this.ClientSize.Width / 2)
                {
                    timerBackMove.Stop();
                    // Resim soldayken bir sonraki hareketin SAĞA olması için yönü hazırla
                    IsLeft = false;
                }
            }
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void BackMove()
        {
            if (timerBackMove.Enabled)
            {
                // Hareket zaten devam ediyorsa, yönü tersine çevir
                IsLeft = !IsLeft;
            }

            // Timer zaten durmuşsa (önceki hareket bitti) veya yeni yön ayarlandıysa (hareket devam ediyordu)
            // Timer'ı başlat
            timerBackMove.Start();
        }
        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {


            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string passwordRef = txtPasswordRef.Text;
            string email = txtEmail.Text;

            if (password != passwordRef)
            {
                MessageBox.Show("Şifreler uyuşmuyor!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(passwordRef) || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Kullanıcı adı, şifre, şifre tekrar ve e-posta alanları boş olamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {

                RegisterUserDto user = new RegisterUserDto()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Username = username,
                    Password = password,
                    Email = email
                };

                _userService.Register(user);
                BackMove();
                txtLoginUsername.Text = username;
                txtFirstName.Clear();
                txtLastName.Clear();
                txtPasswordRef.Clear();
                txtPassword.Clear();
                txtUsername.Clear();
                txtEmail.Clear();
                MessageBox.Show("Kayıt işlemi başarılı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kayıt işlemi sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            string username = txtLoginUsername.Text;
            string password = txtLoginPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Kullanıcı adı ve şifre alanları boş olamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                bool loginSuccess = _userService.Login(username, password);
                if (loginSuccess)
                {
                    txtLoginUsername.Clear();
                    txtLoginPassword.Clear();
                    MessageBox.Show("Giriş başarılı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Kullanıcı adı veya şifre yanlış.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Giriş işlemi sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmRegister_Load(object sender, EventArgs e)
        {

        }

        private void pictBack_Click(object sender, EventArgs e)
        {

        }

        private void pictBack_Click_1(object sender, EventArgs e)
        {

        }

        private void pictBack_Click_2(object sender, EventArgs e)
        {

        }

        private void lblNext0_Click(object sender, EventArgs e)
        {
            BackMove();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            BackMove();
        }
    }
}
