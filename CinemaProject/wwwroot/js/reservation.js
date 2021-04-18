var Reservation_Module = (function () {
   function Init() {
      var reservationButtons = $("#avaiable-broadcasts tr td #make-reservation");
      $.each(reservationButtons, function (index, element) {
         $(element).click(function () {
            var numberOfSelectedSeats = $(this).siblings().val();
            var href = $(this).attr("href");
            $(this).attr("href", href + "?numberOfSelectedSeats=" + numberOfSelectedSeats);
         });
      });

   }

   return {
      Init: function () {
         Init();
      },
   };
})();
