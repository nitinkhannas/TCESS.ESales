$(document).ready(function () {
    var displayAlert = function () {
        if (typeof Page_Validators == 'undefined') return;

        var groups = [];
        for (i = 0; i < Page_Validators.length; i++)
            if (!Page_Validators[i].isvalid) {
                if (!groups[Page_Validators[i].validationGroup]) {
                    ValidationSummaryOnSubmit(Page_Validators[i].validationGroup);
                    groups[Page_Validators[i].validationGroup] = true;
                }
            }
    };

    displayAlert();

    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(
function () {
    displayAlert();
});
}
);