document.addEventListener("DOMContentLoaded", function () {
    const loginForm = document.getElementById("loginForm");
    if (!loginForm) return;

    loginForm.addEventListener("submit", async function (event) {
        event.preventDefault();

        const email = document.getElementById("email")?.value.trim();
        const password = document.getElementById("password")?.value.trim();

        const emailError = document.getElementById("emailLoginError");
        const passwordError = document.getElementById("passwordLoginError");

        [emailError, passwordError].forEach(error => error.innerText = "");

        if (!email || !password) {
            showToast("Lütfen e-posta ve şifre alanlarını doldurun.", "warning");
            if (!email) emailError.innerText = "E-posta alanı boş olamaz";
            if (!password) passwordError.innerText = "Şifre alanı boş olamaz";
            return;
        }

        const loginData = { email, password };
        const data = await sendRequest('https://localhost:7090/api/Auth/login', 'POST', loginData);

        if (data && data.token) {
            showToast("Giriş başarılı! Hoş geldiniz.", "success");
            loginForm.reset();
            // Ana sayfaya 3 sn sonra yönlendir 
            setTimeout(() => {
                window.location.replace('/Default/Index');
            }, 3000);
        } else if (data?.errors) {


            if (data.errors.Email) {
                emailError.innerText = data.errors.Email;
            }
            if (data.errors.Password) {
                passwordError.innerText = data.errors.Password;
            }
        }
        else if (data?.message) {
            showToast(data.message, "danger");
        } else {
            showToast("Giriş başarısız! Lütfen tekrar deneyin.", "danger");

        }


    });
});

