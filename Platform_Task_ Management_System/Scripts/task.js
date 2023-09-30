
    function DeleteTask(TaskID) {
        $.ajax({
            url: "https://localhost:44343/Tasks/DeleteTask?id=" + TaskID,
            type: "GET",
            dataType: "json",
            success: function () {
                console.log("delete success");
                window.location.href = "https://localhost:44343/Tasks/Index"


                setTimeout(swal("Done!", "It was succesfully Deleted!", "success"), 500);

            },


            error: function (err) {
                alert(err);
                swal("Error Deleting!", "Please try again", "error");

            }

        });
    }
        //end of DeleteBilDeltials click
    //$("#TaskTable td").map(function () {
        //    debugger;
        //    if ($('table tr td:nth-child(9)').text().replace(/ /g, "") == '\nComplete\n') $(this).css("background-color", "green");
        //    if ($('table tr td:nth-child(9)').text().replace(/ /g, "") == '\nPending\n') $(this).css("background-color", "orange");
        //    if ($('table tr td:nth-child(9)').text().replace(/ /g, "") == '\nPending\n') $(this).css("background-color", "blue");
        //    if ($('table tr td:nth-child(9)').text().replace(/ /g, "") == '\nCanceled\n') $(this).css("background-color", "red");
        //    else {
        //        $(this).css("background-color", "none");
        //    }

        //})
    //    applyBGColor();
    //function applyBGColor() {
    //    debugger;
    //    if ($('table tr td:nth-child(9)').text().replace(/ /g, "") == '\nComplete\n') $('table tr td:nth-child(9)').css("background-color", "green");
    //    if ($('table tr td:nth-child(9)').text().replace(/ /g, "") == '\nPending\n') $('table tr td:nth-child(9)').css("background-color", "orange");
    //    if ($('table tr td:nth-child(9)').text().replace(/ /g, "") == '\nPending\n') $('table tr td:nth-child(9)').css("background-color", "blue");
    //    if ($('table tr td:nth-child(9)').text().replace(/ /g, "") == '\nCanceled\n') $('table tr td:nth-child(9)').css("background-color", "red");
    //    ////else {
    //    //    $(this).css("background-color", "none");
    //    //}
    //}
