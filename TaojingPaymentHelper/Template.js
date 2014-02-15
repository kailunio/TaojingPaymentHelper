var helper = new function (){
    for (var i = 0; i < 17; i++) {
        jQuery('#addUser').click();
    }
    this.count = 0;
    this.accInputs = jQuery('.i-text.account-display');
    this.prizeInputs = jQuery('.i-text.i-prize.amount');

    this.fill = function (acc, prize) {
        var accIn = jQuery(this.accInputs[this.count]);
        var prizeIn = jQuery(this.prizeInputs[this.count]);
        accIn.focus();
        prizeIn.focus();
        accIn.val(acc);
        prizeIn.val(prize);
        accIn.focus();
        prizeIn.focus();
        this.count++;
    };
};
