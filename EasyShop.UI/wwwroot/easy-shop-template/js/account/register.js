document.addEventListener("DOMContentLoaded", function () {
    const registerForm = document.getElementById("registerForm");
    if (!registerForm) return;

    registerForm.addEventListener("submit", async function (event) {
        event.preventDefault();


        const firstName = document.getElementById("firstName")?.value.trim() || "";
        const lastName = document.getElementById("lastName")?.value.trim() || "";
        const email = document.getElementById("emailRegister")?.value.trim() || "";
        const password = document.getElementById("passwordRegister")?.value.trim() || "";

        const firstNameError = document.getElementById("firstNameError");
        const lastNameError = document.getElementById("lastNameError");
        const emailError = document.getElementById("emailRegisterError");
        const passwordError = document.getElementById("passwordRegisterError");

        [firstNameError, lastNameError, emailError, passwordError].forEach(error => error.innerText = "");


        if (!firstName || !lastName || !email || !password) {
            showToast("Lütfen tüm alanları doldurun.", "warning");
            if (!firstName) firstNameError.innerText = "Ad alanı boş olamaz.";
            if (!lastName) lastNameError.innerText = "Soyad alanı boş olamaz.";
            if (!email) emailError.innerText = "E-posta alanı boş olamaz.";
            if (!password) passwordError.innerText = "Şifre alanı boş olamaz.";

            return;
        }

        const registerData = { firstName, lastName, email, password };
        const data = await sendRequest('https://localhost:7090/api/Auth/register', 'POST', registerData);
        /*if (data?.success) {*/
        if (data && data.token) {
            showToast("Kayıt başarılı! Giriş yapabilirsiniz.", "success");
            registerForm.reset();
            setTimeout(() => {
                var loginTab = new bootstrap.Tab(document.getElementById('login-tab'));
                loginTab.show();
            },1500);
        } else if (data?.errors) {

            if (data.errors.FirstName) {
                firstNameError.innerText = data.errors.FirstName.join(" ");
            }
            if (data.errors.LastName) {
                lastNameError.innerText = data.errors.LastName.join(" ");
            }
            if (data.errors.Email) {
                emailError.innerHTML = "<ul>" + data.errors.Email.map(err => `<li>${err}</li>`).join('') + "</ul>";
            }
            if (data.errors.Password) {
                passwordError.innerHTML = "<ul>" + data.errors.Password.map(err => `<li>${err}</li>`).join('') + "</ul>";
            }
        }
        else if (data?.message) {
            showToast(data.message, "danger");
        } else {
            showToast("Kayıt başarısız! Lütfen tekrar deneyin.", "danger");

        }
    });
});
