var Review_Module = (function () {
   function Init() {
      LoadReviews();

      $("#add-review").click(function () {
         var movieId = $("#movie-id").val();
         var review = $("#movie-add-review").val();
         var reviewViewModel = { MovieId: movieId, Review: review };
         $.ajax({
            type: "POST",
            url: "/Movie/AddReview/",
            contentType: "application/json",
            traditional: true,
            data: JSON.stringify(reviewViewModel),
            success: function (result) {
               if (result) {
                  var reviewContainer = $("#reviews");
                  reviewContainer.empty();
                  LoadReviews();
                  $("#movie-add-review").val("");
               }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
            }
         });
      })
   }

   function LoadReviews() {
      $("#loader-for-reviews").show();
      var movieId = $("#movie-id").val();
      setTimeout(function () {
         $.ajax({
            type: "GET",
            url: "/Movie/GetReviewsByMovieId/" + movieId,
            contentType: "application/json",
            success: function (result) {
               var reviewContainer = $("#reviews");
               reviewContainer.empty();
               $("#loader-for-reviews").hide();
               if (result.length <= 25) {
                  reviewContainer.append($("<p></p>").text("Niciun review."));
               } else {
                  reviewContainer.append(result);
               }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
            }
         });
      }, 500);
      
   }

   return {
      Init: function () {
         Init();
      },
   };
})();
