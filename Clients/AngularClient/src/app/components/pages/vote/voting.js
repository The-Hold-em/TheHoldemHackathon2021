$(document).ready(() => {
  $('input[type="radio"]').on("change", function (e) {
    var candidate = $(e.target).parents(".candidate")[0];
    $(".candidate").removeClass("active");
    $(candidate).addClass("active");
  });
  $(".circle").on("click", function (e) {
    var radio = $(e.target).find('input[type="radio"]');
    radio.checked = true;
    $(radio).trigger("change");
  });
});
