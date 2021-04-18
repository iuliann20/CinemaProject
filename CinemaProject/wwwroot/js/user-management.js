var User_Management_Module = (function () {
   function Init() {
      var editButtons = $("#user-table tr td #edit-user");
      $.each(editButtons, function (index, element) {
         $(element).click(function () {
            $(this).addClass("d-none")
            var saveChangesButton = $(this).parent().find("#save-changes");
            var deleteButton = $(this).parent().find("#delete-user");
            saveChangesButton.removeClass("d-none");
            saveChanges($(saveChangesButton))
            deleteButton.addClass("d-none");
            var row = $(this).parent().parent();
            var fields = row.find("td");
            $.each(fields, function (index, field) {
               var fieldName = $(field).attr("name");
               if (fieldName != "userId" && fieldName != "actions") {
                  if (fieldName == "roleName") {
                     $.ajax({
                        type: "GET",
                        url: "/UserManagement/GetRoles",
                        contentType: "application/json",
                        success: function (result) {
                           generateRoleSelect($(field), result)
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                        }
                     });
                  } else {
                     changeFieldsToInputs($(field));
                  }
               }
            });
         });
      });

   }

   function saveChanges(saveChangesButton) {
      saveChangesButton.click(function () {
         var row = $(this).parent().parent();
         var inputs = row.find("td input");
         var userId = row.data("user-id");
         var role = $(row.find("select")).val();
         var userViewModel = { UserId: userId, FirstName: "", LastName: "", Email: "", PhoneNumber: "", RoleName: role };
         $.each(inputs, function (index, input) {
            switch ($(input).parent().attr("name")) {
               case "firstName":
                  userViewModel.FirstName = $(input).val();
                  break;
               case "lastName":
                  userViewModel.LastName = $(input).val();
                  break;
               case "email":
                  userViewModel.Email = $(input).val();
                  break;
               case "phoneNumber":
                  userViewModel.PhoneNumber = $(input).val();
                  break;
            }
         });
         if (validateViewModel(userViewModel)) {
            $.ajax({
               type: "POST",
               url: "/UserManagement/EditUser/",
               contentType: "application/json",
               traditional: true,
               data: JSON.stringify(userViewModel),
               success: function (result) {
                  if (result) {
                     saveChangesButton.addClass("d-none");
                     $.each(saveChangesButton.siblings(), function (index, siblig) {
                        $(siblig).removeClass("d-none");
                     });
                     $.each(inputs, function (index, input) {
                        changeInputsToFields($(input));
                     });
                     var newRole = $(row.find("select")).val();
                     if (newRole !== null && newRole !== undefined) {
                        var selectTd = row.find("td[name='roleName']");
                        selectTd.empty();
                        selectTd.text(newRole);
                     }
                  }
               },
               error: function (XMLHttpRequest, textStatus, errorThrown) {
               }
            });
         }
      });
   }

   function validateViewModel(userViewModel) {
      if (userViewModel.UserId == null) {
         return false;
      }

      if (userViewModel.FirstName === "") {
         return false;
      }

      if (userViewModel.LastName === "") {
         return false;
      }

      if (userViewModel.Email === "") {
         return false;
      }

      if (userViewModel.PhoneNumber === "") {
         return false;
      }

      if (userViewModel.RoleName === "") {
         return false;
      }

      return validateEmail(userViewModel.Email)
   }

   function validateEmail(email) {
      var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
      return re.test(email);
   }

   function generateRoleSelect(field, result) {
      var fieldText = field.text();
      field.empty();
      var select = $("<select></select>");
      generateOptionsInSelect(select, result)
      select.val(fieldText);
      field.append(select);
   }

   function changeFieldsToInputs(field) {
      var firstNameInput = $("<input/>").attr("type", "text").val(field.text());
      field.empty();
      field.append(firstNameInput);
   }

   function changeInputsToFields(input) {
      var inputText = input.val();
      var inputTd = input.parent();
      inputTd.empty();
      inputTd.text(inputText);
   }

   function generateOptionsInSelect(select, values) {
      select.empty();
      for (let i = 0; i < values.length; i++) {
         select.append($("<option></option>").html(values[i]));
      }
   }

   return {
      Init: function () {
         Init();
      },
   };
})();
