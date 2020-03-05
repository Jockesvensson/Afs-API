
const uri = "api/job";
let jobs = null;


function getCount(data) {
    const el = $("#totalAds");
    let name = "Hudiksvall";

    

    if (data) {
        if (data <= 0) {
            name = "Hudiksvall";
        }
        el.text("Totalt " + data + " annonser i " + name);
    } else {
        el.text("Inga annonser i " + name);
    }

}


$(document).ready(function () {
    getData();

    $("#main-content").hide().fadeIn(500);

    $('#selectNumber').on('click', () => {
        getData()
    });

    //Enable this if you like to close the 'view window' with a mouseclick outside the 'view window'
    $("#overlay").on("click", function () {
        closeView();
    });

    //Makes the 'Stäng annons' button to close the 'view window'
    $("#buttonClose").on("click", function () {
        closeView()
    });
});


function getData() {
    $.ajax({
        type: "GET",
        url: uri,
        cache: false,
        success: function (data) {
            const tBody = $("#list");

            $(tBody).empty();

            getCount(data.length);

            const maxJobsPerPage = $("#selectNumber").val();
           
            $.each(data, function (key, item) {
                if (key < maxJobsPerPage) {
                    const tr = $("<tr></tr>")
                        //const tr = $("<div></div>")
                  
                        .append($("<td></td>").text(item.headline))
                        .append($("<td></td>").text(item.companyName + " - " + item.city))
                        .append($("<td></td>").text(item.workTitle))
                        //.append($("<td></td>").text(item.workingHours + ", " + item.workingDuration))
                        .append($("<td></td>").text(item.applicationDeadline))
                        .append($("<td></td>").text(item.publicationDate));                

                    //.append($("<p></p>").text(item.headline))
                    //.append($("<p></p>").text(item.companyName + " - " + item.city))
                    //.append($("<p></p>").text(item.workTitle))
                    //.append($("<p></p>").text(item.workingHours + ", " + item.workingDuration))
                    //.append($("<p></p>").text("Ansök senast " + item.applicationDeadline))
                    //.append($("<p></p>").text("Publicerad " + item.publicationDate));


                    

                    tr.appendTo(tBody).on("click", function () {
                        viewItem(item.id);

                    });
                 
                }
            });

            

            //$("table > tbody > tr").hide().slice(0, 14).show();
            

            //$(".showFirstPage").on("click", function () {
            //    $("tbody > tr").hide().slice(15, 100);
            //    $("tbody > tr", $(this).prev()).slice(0, 14).show();
            //});
            //$(".showSecondPage").on("click", function () {
            //    $("tbody > tr").hide().slice(0, 14);
            //    $("tbody > tr", $(this).prev()).slice(15, 29).show();

            //});

            //$(".showThirdPage").on("click", function () {
            //    $("tbody > tr").hide().slice(15, 28);
            //    $("tbody > tr", $(this).prev()).slice(29, 43).show();
            //});

 
            jobs = data;
        }
    });

}

function viewItem(id) {
    $.each(jobs, function (key, item) {
        if (item.id === id) {
            
            $("#view-city").text("Ort: " + item.city);
            $("#view-applicationDeadline").text("Sista ansökningsdag: " + item.applicationDeadline);
            $("#view-description").text(item.description);
            
           
        }
    });
   
    $("#view").css({ display: "block" });
    $("#overlay").css({ display: "block" });

}

function closeView() {
    $("#overlay").css({ display: "none" });
    $("#view").css({ display: "none" });
   
   
}







