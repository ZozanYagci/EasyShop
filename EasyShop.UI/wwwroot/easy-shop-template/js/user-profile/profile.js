$(document).ready(function () {
    function handleAjaxForm($form, successSelector, errorSelector) {
        $form.submit(function (e) {
            e.preventDefault();

            const $success = $form.find(successSelector);
            const $error = $form.find(errorSelector);

            $success.addClass("d-none").text("");
            $error.addClass("d-none").text("");
            $form.find("span.text-danger").text("");

            let formData = $form.serialize();
            let actionUrl = $form.attr("action");

            $.ajax({
                url: actionUrl,
                type: "POST",
                data: formData,
                success: function (response) {
                    if (response && response.success) {
                        $success.removeClass("d-none").text(response.message);
                    } else {
                        $error.removeClass("d-none").text(response.message || "Bir hata oluştu, lütfen tekrar deneyin.");
                    }
                },
                error: function (xhr) {
                    if (xhr.status === 400 && xhr.responseJSON && xhr.responseJSON.errors) {
                        let errors = xhr.responseJSON.errors;
                        for (let field in errors) {
                            let errorMessages = errors[field].map(msg => `<li>${msg}</li>`).join("");
                            let errorSpan = $form.find(`span[data-valmsg-for="${field}"]`);
                            if (errorSpan.length) {
                                errorSpan.html(`<ul>${errorMessages}</ul>`);
                            }
                        }
                    } else {
                        $error.removeClass("d-none").text(xhr.responseJSON?.message || "Bir hata oluştu, lütfen tekrar deneyin.");
                    }
                }
            });
        });
    }

    handleAjaxForm($("#editProfileForm"), ".profile-alert-success", ".profile-alert-error");
    handleAjaxForm($("#passwordForm"), ".password-alert-success", ".password-alert-error");

    $(".toggle-password").on("click", function () {
        const $icon = $(this);
        const targetId = $icon.data("target");
        const $input = $("#" + targetId);

        if ($input.length) {
            const type = $input.attr("type") === "password" ? "text" : "password";
            $input.attr("type", type);
            $icon.html(type === "password"
                ? '<i class="fa fa-eye-slash"></i>'
                : '<i class="fa fa-eye"></i>');
        }
    });
});
