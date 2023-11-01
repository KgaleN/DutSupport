
    // Function to show the popup
    function showPopup(subId) {
        var popup = document.getElementById("popup-" + subId);
    popup.style.display = "block";

    }

    // Function to hide the popup
    function hidePopup(subId) {
        var popup = document.getElementById("popup-" + subId);
    popup.style.display = "none";
    }

    // Attach a click event handler to the "assign" buttons using event delegation
    document.addEventListener("click", function (e) {
           if (e.target && e.target.classList.contains("assign-button")) {
               var subId = e.target.getAttribute("data-subid");
              showPopup(subId);
           }
        });

        document.addEventListener("click", function (e) {
            if (e.target && e.target.classList.contains("assign-button")) {
                var subId = e.target.getAttribute("data-id");
                var selectedRadio = document.querySelector('input[name="btnradio-' + subId + '"]:checked').getAttribute("value");
                $("input[name=selectedRadio]").attr('value', selectedRadio);


                showPopup(subId);
            }
        });

    // Attach a click event handler to the "Close" buttons in the popups using event delegation
    document.addEventListener("click", function (e) {
        if (e.target && e.target.classList.contains("close-popup-button")) {
            var subId = e.target.closest(".popup-container").id.split("-")[1];
    hidePopup(subId);
        }
    });




