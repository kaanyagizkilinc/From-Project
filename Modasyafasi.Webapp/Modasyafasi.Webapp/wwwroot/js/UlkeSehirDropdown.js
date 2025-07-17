
(function ($) {
    var ulkeSelect = $("#Ulke");
    var sehirSelect = $("#Sehir");
    var ulkedata = ulkeSelect.data("secili-id");
    var sehirdata = sehirSelect.data("secili-id");

    // Ülkeleri getir
    $.ajax({
        url: "/Home/UlkeGetir",
        type: "GET",
        success: function (data) {
            ulkeSelect.append('<option value="">Ülke Seçiniz</option>')
            ulkeSelect.empty();
            $.each(data, function (index, ulke) {
                var selected = ulke.id == ulkedata ? "selected" : "";
                ulkeSelect.append('<option value="' + ulke.id + '" ' + selected + '>' + ulke.tanim + '</option>');
            });
            if (ulkedata) {
                ulkeSelect.trigger("change");
            }
        },
        error: function () {
        }
    });

    // Ülke değişince şehirleri getir
    ulkeSelect.on("change", function () {
        var ulkeId = $(this).val();
        sehirSelect.empty();
        if (!ulkeId) {
            sehirSelect.append('<option value="">Önce Ülke Seçiniz...</option>');
            return;
        }

        $.ajax({
            url: "/Home/SehirGetir?id=" + ulkeId,
            type: "GET",
            success: function (data) {
                sehirSelect.empty();
                sehirSelect.append('<option value="">Şehir Seçiniz</option>');
                $.each(data, function (index, sehir) {
                    var selected = sehir.id == sehirdata ? "selected" : "";
                    sehirSelect.append('<option value="' + sehir.id + '" ' + selected + '>' + sehir.tanim + '</option>');
                });
            },
            error: function () {
                sehirSelect.empty().append('<option value="">Şehirler Yüklenemedi</option>');
            }
        });
    });

})(jQuery);
