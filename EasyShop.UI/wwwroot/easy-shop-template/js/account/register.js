document.addEventListener("DOMContentLoaded", function () {
    const registerForm = document.getElementById("registerForm");
    if (!registerForm) return;

    registerForm.addEventListener("submit", async function (event) {
        event.preventDefault();


        const firstName = document.getElementById("firstName")?.value.trim();
        const lastName = document.getElementById("lastName")?.value.trim();
        const email = document.getElementById("emailRegister")?.value.trim();
        const password = document.getElementById("passwordRegister")?.value.trim();

        if (!firstName || !lastName || !email || !password) {
            showToast("Lütfen tüm alanları doldurun.", "warning");
            return;
        }

        const registerData = { firstName, lastName, email, password };
        const data = await sendRequest('https://localhost:44372/api/Auth/register', 'POST', registerData);
        /*if (data?.success) {*/
        if (data && data.token) {
            showToast("Kayıt başarılı! Giriş yapabilirsiniz.", "success");
            setTimeout(() => window.location.replace('/Account/Login'), 1500);
        } else {
            showToast("Kayıt başarısız! Lütfen tekrar deneyin.", "danger");

        }
    });
});
