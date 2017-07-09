(function () {
    window.addEvent('domready', function () {
        new Planner();
    });
    var Planner = new Class({
        form: undefined,
        fieldsets: [],
        response: undefined,
        error_messages: [],
        initialize: function () {
            this.get_form();
            this.get_fieldsets();
            this.events();
        },
        get_form: function () {
            this.form = document.getElement('form[action="/planner/save"]');
        },
        get_fieldsets: function () {
            this.fieldsets = this.form.getElements('fieldset');
        },
        events: function () {
            this.form.addEvent('submit', this.do_submit.bind(this));
        },
        do_submit: function () {
            this.error_messages.each(function (message) {
                message.dispose();
            });
            this.error_messages = [];
            this.form.getElements('.error,.field-error').removeClass('error').removeClass('field-error');
            new Request.JSON({
                url: this.form.get('action'),
                method: this.form.get('method'),
                data: this.form.toQueryString(),
                onSuccess: this.on_success.bind(this),
                onFailure: this.on_fail.bind(this),
                onError: this.on_fail.bind(this)
            }).send();
            return false;
        },
        on_fail: function (x) {
            console.log(x);
            console.log(x.responseText);
        },
        on_success: function (response) {
            this.response = response;
            if (this.response.success) {
                this.success();
            } else {
                this.errors();
            }
        },
        success: function () {
            window.Extensions.hexalert('Thanks!', true);
        },
        errors: function () {
            var messages = messages = this.response.messages.clone(),
                scroll_to = null,
                first_error = null;
            this.response.errors.each(function (field) {
                if (field == 'SESSIONKEY_FORMKEY') return;
                var elements = this.form.getElements('[name="' + field + '"],[name="' + field + '[]"]');
                if (elements.length > 0) {
                    elements[0].getParents('fieldset').shift().addClass('error');
                    if (first_error === null) {
                        first_error = elements[0];
                    }
                }
                if (elements.length > 1 || elements.length == 0) return;
                elements.shift().getParents('.form-el').shift().addClass('field-error');
            }, this);
            first_error.focus();
            first_error.select();
            this.fieldsets.each(function (fieldset) {
                var message;
                if (!fieldset.hasClass('error')) return false;
                if (messages.length == 0) messages = this.response.messages.clone();
                message = new Element('div').addClass('error');
                message.set('html', messages.shift());
                fieldset.getElement('h2').grab(message, 'after');
                this.error_messages.push(message);
                if (scroll_to === null) scroll_to = fieldset;
            }, this);
            new Fx.Scroll(window, {
                duration: 400
            }).start(0, scroll_to.getCoordinates().top - 20);
        }
    });
})();