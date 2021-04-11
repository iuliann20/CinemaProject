var Add_Movie_Module = (function () {
   function Init() {
      $('form').bind("keypress", function (e) {
         if (e.keyCode == 13) {
            e.preventDefault();
            return false;
         }
      });
      $("#add-actor").click(function (e) {
         var input = $('<input>').attr({
         }).addClass("form-control mb-2").attr("placeholder", "Enter actor name...");
         $("#actors-grid").append(input);
      });
      $("form").submit(function (event) {
         var actorValues = [];
         $.each($("#actors-grid input"), function (index, input) {
            var actorName = $(input).val();
            if (actorName !== "") {
               actorValues.push(actorName);
            }
         });
         generateOptionsInSelect($("#actors"), actorValues);
      });
   }

   function generateOptionsInSelect(select, values) {
      select.empty();
      for (let i = 0; i < values.length; i++) {
         select.append($("<option></option>").html(values[i]));
      }
      select.val(values);
   }

   return {
      Init: function () {
         Init();
      },
   };
})();
