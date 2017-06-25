;
(function (window, Extensions, undefined) {
    'use strict';
    window.Extensions = Extensions;
    Extensions.Inst = Extensions.Inst || {};
    Extensions.isMobile = [Browser.Platform.webos, Browser.Platform.ios, Browser.Platform.android].contains(true);
    Extensions.CustomForm = new Class({
        Implements: Options,
        options: {},
        initialize: function (form, options) {
            this.setOptions(options);
            this.form = form;
            this.elements = this.form.getElements(this.options.elements.join(', '));
            this.zCount = 500;
            var _this = this;
            this.elements.each(function (el) {
                var type = '_' + el.get('data-type');
                if (typeof _this[type] === 'function') {
                    _this[type](el);
                }
            });
        },
        _hoverInfo: function (el) {
            var info = el.getElement('.info');
            if (info === null) {
                return;
            }
            var _this = this,
                container = el.getParents('li')[0],
                position = el.getStyle('position'),
                zIndex = container.getStyle('z-index'),
                timeout;

            function focusOn() {
                clearTimeout(timeout);
                container.setStyles({
                    position: 'relative',
                    zIndex: _this.zCount++
                });
                info.store('hovered', true).setStyle('display', 'block');
                setTimeout(function () {
                    info.addClass('active');
                }, 0);
            }

            function focusOff() {
                info.store('hovered', false).removeClass('active');
                timeout = setTimeout(function () {
                    info.setStyle('display', 'none');
                    container.setStyles({
                        position: position,
                        zIndex: zIndex
                    });
                }, 275);
            }
            el.addEvents({
                mouseenter: focusOn,
                mouseleave: focusOff
            });
        },
        _checkbox: function (el) {
            var input = el.getElement('input');
            input.addEvent('change', function () {
                el.toggleClass('active');
            });
            this._ieCheck(el, input);
            this._hoverInfo(el);
        },
        _radio: function (el) {
            var input = el.getElement('input');
            input.addEvent('change', function () {
                this.form.getElements('input[name="' + input.get('name') + '"]').each(function (radio) {
                    radio.getParent().removeClass('active');
                });
                el.addClass('active');
            });
            this._ieCheck(el, input);
            this._hoverInfo(el);
        },
        _file: function (el) {
            var input = el.getElement('input');
            var label = el.getElement('.custom-file-label');
            var file = el.getElement('.custom-file-file');
            input.addEvent('change', function (e) {
                file.set('text', this.files[0].name);
            });
        },
        _ieCheck: function (el, input) {
            if (Browser.ie && Browser.version < 9) {
                el.addEvent('click', function () {
                    input.set('checked', !input.get('checked'));
                });
            }
        },
        _select: function (el) {
            var _this = this,
                select = el.getElement('select'),
                container = el.getParent(),
                options = select.getElements('option'),
                hasDisabled = options[0].getProperty('disabled'),
                i = +hasDisabled,
                children = [];
            var cStyle = container.getStyles('position', 'z-index'),
                elStyle = el.getStyles('position', 'z-index');
            var info = new Element('ul', {
                'class': 'info'
            });
            var inner = new Element('span', {
                'class': 'inner',
                text: options[select.selectedIndex].get('text')
            });
            for (; i < options.length; i++) {
                children.push(new Element('li').adopt(new Element('a', {
                    href: '#',
                    text: options[i].get('text')
                })))
            }
            info.adopt(children);
            el.adopt(inner, new Element('i', {
                'class': 'ss-icon ss-standard',
                html: '&#xF501;'
            }), info);
            info.getElements('a').addEvent('click', function (e) {
                e.preventDefault();
                var index = children.indexOf(this.getParent()) + (hasDisabled ? 1 : 0);
                select.set('value', options[index].get('value'));
                inner.set('text', this.get('text'));
                info.removeClass('active');
                setTimeout(function () {
                    if (!info.retrieve('hovered')) {
                        info.setStyle('display', 'none');
                    }
                }, 275);
            });
            this._hoverInfo(el);
        }
    });
    Extensions.Topper = new Class({
        Implements: [Options, Events],
        options: {
            cutOff: 400
        },
        transitions: {
            scroll: new Fx.Scroll(document.body),
            clicker: undefined
        },
        busy: false,
        initialize: function (clicker, options) {
            this.setOptions(options);
            this.clicker = clicker;
            this.transition = this.cutOff = this.options.cutOff;
            setTimeout(function () {
                this._ScrollSpy();
            }.bind(this), 250);
            this._events();
        },
        _ScrollSpy: function () {
            var _this = this,
                min = 400,
                spy = {};

            function updateBounds() {
                spy.options.min = document.id('footer').getCoordinates().top - (window.getSize().y - 100);
            }
            this.transitions.clicker = new Fx.Morph(this.clicker, {
                duration: 500,
                onComplete: function () {
                    _this.busy = false;
                }
            });
            spy = new ScrollSpy({
                min: 400,
                onEnter: function () {
                    _this.transitions.clicker.set({
                        display: 'block',
                        opacity: 0
                    });
                    setTimeout(function () {
                        _this.transitions.clicker.start({
                            opacity: 1
                        });
                    }, 0);
                },
                onLeave: function () {
                    _this.transitions.clicker.start({
                        opacity: 0
                    });
                    setTimeout(function () {
                        _this.clicker.setStyle('display', 'none');
                    }, 500);
                }
            });
            if (document.id('work-projects') != null) {
                updateBounds();
                window.addEvent('resize', updateBounds);
            }
        },
        _events: function () {
            var _this = this;
            this.clicker.addEvent('click', function (e) {
                e.preventDefault();
                if (_this.busy) {
                    return;
                }
                _this.busy = true;
                _this.transitions.scroll.start(0, 0);
                _this.clicker.blur();
            });
        }
    });
    Extensions.Slider = new Class({
        Implements: [Options, Events],
        options: {
            start: 0,
            duration: 500,
            markers: true,
            autoPlay: false,
            delay: 4000,
            crossFade: true,
            doHeight: true,
            transitions: {
                inPrep: {
                    display: 'block'
                },
                goIn: {
                    opacity: 1,
                    transform: 'none'
                },
                out: {
                    opacity: 0
                }
            }
        },
        transitions: {
            slider: undefined
        },
        busy: false,
        initialize: function (slider, options) {
            var _this = this;
            this.setOptions(options);
            this.slider = slider;
            this.slides = slider.getChildren();
            this.markers = [];
            this.num = this.slides.length;
            this.active = this.options.start;
            this.slides[this.active].setStyle('display', 'block');
            if (this.options.markers) {
                this._addMarkers();
            }
            if (this.options.autoPlay) {
                this._autoPlay();
            }
            if (this.options.doHeight) {
                this.transitions.slider = new Fx.Morph(this.slider, {
                    duration: this.options.duration,
                    onComplete: function () {
                        _this.slider.setStyle('height', 'auto');
                    }
                });
            }
        },
        _addMarkers: function () {
            this.markerList = new Element('ul', {
                'class': 'list-inline-block slider-markers'
            });
            for (var i = 0; i < this.num; i++) {
                var a = new Element('a', {
                    href: '#'
                }).store('index', i);
                if (i === this.active) {
                    a.addClass('active');
                }
                this.markers.push(new Element('li').adopt(a));
                this._addMarkerEvents(a);
            }
            this.markerList.adopt(this.markers);
            this.markerList.inject(this.slider, 'after');
        },
        _getNextIndex: function (add) {
            var next = this.active + (add || 1);
            if (next < 0) {
                next = this.num - 1;
            } else if (next > this.num - 1) {
                next = 0;
            }
            return next;
        },
        _autoPlay: function () {
            var _this = this;
            this.interval = setTimeout(function () {
                _this.slide(_this._getNextIndex());
            }, _this.options.delay);
        },
        _addMarkerEvents: function (marker) {
            var _this = this;
            marker.addEvent('click', function (e) {
                e.preventDefault();
                _this.slide(this.retrieve('index'));
            });
        },
        slide: function (index) {
            var _this = this;
            if (index === this.active || this.busy) {
                return;
            }
            this.busy = true;
            if (this.options.markers) {
                [this.markers[this.active], this.markers[index]].each(function (el) {
                    el.getElement('a').toggleClass('active');
                });
            }
            if (this.options.crossFade) {
                this._hideCurrent();
            } else {
                setTimeout(function () {
                    _this._hideCurrent();
                }, this.options.duration + 50);
            }
            this._showNext(index);
        },
        _hideCurrent: function () {
            var _this = this;
            if (this.options.doHeight) {
                this.slider.setStyle('height', _this.slider.getSize().y);
            }
            var morphOut = new Fx.Morph(_this.slides[_this.active], {
                duration: _this.options.duration
            }),
                callback = setTimeout(function () {
                    this.setStyles({
                        display: 'none',
                        opacity: 0,
                        position: 'static'
                    });
                }.bind(_this.slides[_this.active]), _this.options.duration + 50);
            morphOut.set({
                position: 'absolute'
            }).start(_this.options.transitions.out);
        },
        _showNext: function (index) {
            var _this = this,
                morphIn = new Fx.Morph(_this.slides[index], {
                    duration: _this.options.duration
                }),
                callback = setTimeout(function () {
                    _this.active = index;
                    _this.busy = false;
                    if (_this.options.autoPlay) {
                        _this._autoPlay();
                    }
                }, _this.options.duration + 50);
            morphIn.set(_this.options.transitions.inPrep);
            setTimeout(function () {
                morphIn.start(Object.merge(_this.options.transitions.goIn));
            }, 0);
            if (this.options.doHeight) {
                this.transitions.slider.start({
                    height: this.slides[index].getSize().y
                });
            }
        }
    });
    Extensions.Modal = new Class({
        Implements: [Options, Events],
        options: {
            duration: 300,
            linkSelector: '[data-modal-link]'
        },
        transitions: {
            wrap: undefined,
            modal: undefined
        },
        modalClasses: ['no-footer', 'footer-one-col', 'body-one-col', 'not-group', 'wide-first', 'footer-minimal'],
        inProgress: false,
        initialize: function (modal, options) {
            this.setOptions(options);
            this.modal = modal;
            this.wrap = modal.getParent();
            this.content = document.id('modal-content');
            this.footer = document.id('modal-footer');
            this.close = document.id('modal-close');
            this.left = document.id('modal-left');
            this.right = document.id('modal-right');
            this.html = document.getElement('html');
            this.overlay = document.id('modal-overlay');
            this.group = false;
            this.isActive = false;
            this.currIndex = false;
            var bodyCols = this.content.getElements('.grid-7'),
                footerCols = this.footer.getElements('.grid-7');
            this.sections = {
                title: this.content.getElement('h2'),
                tagline: this.content.getElement('h3'),
                bodyCol1: bodyCols[0],
                bodyCol2: bodyCols[1],
                footerCol1: footerCols[0],
                footerCol2: footerCols[1]
            };
            this._transitions();
            this._events(document.getElements(this.options.linkSelector));
        },
        _callback: function () { },
        _transitions: function () {
            var _this = this;
            this.transitions.wrap = new Fx.Morph(_this.overlay, {
                duration: this.options.duration,
                onComplete: function () {
                    _this.inProgress = false;
                }
            });
            this.transitions.modal = new Fx.Morph(_this.modal, {
                duration: _this.options.duration,
                onComplete: function () {
                    _this._callback();
                    _this.inProgress = false;
                }
            });
        },
        addContent: function (content) {
            this._resetClasses();
            var addClasses = [],
                bodyCols = content.getElements('div[data-m-body-col]'),
                footerCols = content.getElements('div[data-m-footer-col]');
            if (content.get('data-large') !== null) {
                addClasses.push(this.modalClasses[4]);
            }
            if (content.get('data-footer-minimal') !== null) {
                addClasses.push(this.modalClasses[5]);
            }
            if (!footerCols.length) {
                addClasses.push(this.modalClasses[0]);
            } else if (footerCols.length < 2) {
                addClasses.push(this.modalClasses[1]);
                this.sections.footerCol1.set('html', footerCols[0].get('html'));
            } else {
                this.sections.footerCol1.set('html', footerCols[0].get('html'));
                this.sections.footerCol2.set('html', footerCols[1].get('html'));
            }
            if (bodyCols.length < 2) {
                addClasses.push(this.modalClasses[2]);
                this.sections.bodyCol1.set('html', bodyCols[0].get('html'));
            } else {
                this.sections.bodyCol1.set('html', bodyCols[0].get('html'));
                this.sections.bodyCol2.set('html', bodyCols[1].get('html'));
            }
            this.sections.title.set('html', content.getElement('div[data-m-title]').get('html'));
            this.sections.tagline.set('html', content.getElement('div[data-m-tagline]').get('html'));
            if (!this.group) {
                this.modal.addClass(this.modalClasses[3]);
            }
            this.modal.addClass(addClasses.join(' '));
            content.destroy();
        },
        show: function (content) {
            var _this = this;
            this.inProgress = true;
            if (!this.isActive) {
                this.isActive = true;
                this.addContent(content);
                this.html.setStyle('overflow', 'hidden');
                this.transitions.wrap.set({
                    display: 'block'
                });
                this.wrap.setStyle('display', 'block');
                this.transitions.modal.set({
                    opacity: 0,
                    transform: 'scale(.97)'
                });
                setTimeout(function () {
                    _this.transitions.wrap.start({
                        opacity: .1
                    });
                    _this.transitions.modal.start({
                        opacity: 1,
                        transform: 'none'
                    });
                }, 50);
                window.addEvent('keyup', this._keyEventsHandler);
            } else {
                this._callback = function () {
                    _this.addContent(content);
                    _this.transitions.modal.set({
                        transform: 'scale(.97)'
                    });
                    setTimeout(function () {
                        _this.transitions.modal.start({
                            transform: 'none',
                            opacity: 1
                        });
                    }, 50);
                    _this._callback = function () { };
                };
                this.transitions.modal.start({
                    transform: 'scale(.97)',
                    opacity: 0
                });
            }
        },
        _resetClasses: function () {
            this.modalClasses.each(function (className) {
                this.modal.removeClass(className);
            }.bind(this));
        },
        hide: function () {
            var _this = this;
            this.html.setStyle('overflow', 'auto');
            _this.transitions.wrap.start({
                opacity: 0
            });
            _this.transitions.modal.start({
                opacity: 0,
                transform: 'scale(1.03)'
            });
            this._callback = function () {
                _this.wrap.setStyle('display', 'none');
                _this.overlay.setStyle('display', 'none');
                ['group', 'currIndex', 'isActive'].each(function (prop) {
                    _this[prop] = false;
                });
                _this._callback = function () { };
            };
            window.removeEvents('keyup', this._keyEventsHandler);
        },
        showNext: function (forward) {
            if (!this.group || this.inProgress) {
                return false;
            }
            this.currIndex += (forward === true ? 1 : -1);
            if (this.currIndex < 0) {
                this.currIndex = this.group.length - 1;
            } else if (this.currIndex >= this.group.length) {
                this.currIndex = 0;
            }
            this._request(this.group[this.currIndex].get('data-modal'));
        },
        _request: function (location) {
            var _this = this;
            new Request.HTML({
                url: location,
                onSuccess: function (tree, elements, html) {
                    _this.show(elements[0]);
                }
            }).send();
        },
        _events: function (links) {
            var _this = this;
            links.each(function (link) {
                link.addEvent('click', function (e) {
                    e.preventDefault();
                    if (_this.inProgress) return;
                    var group = link.get('data-m-group') || false;
                    if (group) {
                        _this.group = document.getElements('a[data-m-group=' + group + ']');
                        if (_this.group.length == 1) {
                            _this.left.setStyle('display', 'none');
                            _this.right.setStyle('display', 'none');
                        }
                        _this.currIndex = _this.group.indexOf(link);
                    }
                    _this._request(link.get('data-modal'));
                });
            });
            this.close.addEvent('click', function (e) {
                e.preventDefault();
                _this.hide();
            });
            this.wrap.addEvent('click', function (e) {
                _this.hide();
            });
            this.modal.addEvent('click', function (e) {
                e.stopPropagation();
            });
            this._keyEventsHandler = this._keyEvents.bind(this);
            [this.left, this.right].each(function (arrow) {
                arrow.addEvent('click', function (e) {
                    e.preventDefault();
                    _this.showNext(arrow.match(_this.right));
                });
            });
        },
        _keyEvents: function (e) {
            function prevent() {
                e.preventDefault();
                e.stopPropagation();
            }
            switch (e.key) {
                case 'esc':
                    this.hide();
                    break;
                case 'left':
                    prevent();
                    this.showNext(false);
                    break;
                case 'right':
                    prevent();
                    this.showNext(true);
                    break;
            }
        },
        addLinks: function (links) {
            this._events(links);
        }
    });
    Extensions.Hexagons = new Class({
        Implements: [Options, Events],
        options: {
            duration: 250
        },
        initialize: function (list, options) {
            this.setOptions(options);
            this.list = list;
            this.listItems = list.getElements('div.hex');
            this.hexLinks = list.getElements('a');
            this.section = this.list.getParents('section')[0];
            this._hoverEvents();
            if (this.section && this.section.get('data-interactive') !== null) {
                this.interact();
            }
        },
        _doFade: function (elems, toOpacity) {
            elems.each(function (el) {
                el.set('tween', {
                    duration: 250
                }).get('tween').start('opacity', toOpacity);
            });
        },
        interact: function () {
            this.section.addEvent('interact', this.fadeIn.bind(this));
        },
        fadeIn: function () {
            var lis = this.listItems;
            this.fadeInAndRemove.delay(100, this, [lis]);
        },
        fadeInAndRemove: function (lis) {
            var show = Math.floor(Math.random() * (lis.length - 1));
            lis[show].set('tween', {
                duration: 400
            }).tween('opacity', 1);
            lis[show] = null;
            lis = lis.clean();
            if (lis.length === 0) {
                return;
            }
            this.fadeInAndRemove.delay(100, this, [lis]);
        },
        _hoverEvents: function () {
            var _this = this,
                hoverOpacity = Modernizr.opacity ? .88 : 1;
            this.hexLinks.each(function (a) {
                var toFade, opacityArr;
                if (a.getParent().hasClass('invert')) {
                    toFade = a.getElements('.inner, .after');
                    opacityArr = [hoverOpacity, 0];
                } else {
                    toFade = a.getElements('.after');
                    opacityArr = [hoverOpacity, 1];
                }
                a.addEvents({
                    mouseenter: function () {
                        _this._doFade(toFade, opacityArr[0]);
                    },
                    mouseleave: function () {
                        _this._doFade(toFade, opacityArr[1]);
                    }
                });
            });
        },
    });
    Extensions.Nav = new Class({
        initialize: function (nav) {
            var highlight = nav.getElement('a[href="' + window.location.pathname + '"]');
            if (highlight) {
                highlight.addClass('active');
            }
        }
    });
    Extensions.Interactive = new Class({
        Implements: [Options, Events],
        options: {
            topMinus: 200
        },
        hasFired: false,
        initialize: function (section, options) {
            if (Extensions.isMobile) return false;
            this.setOptions(options);
            this.section = section;
            this.coords = section.getCoordinates();
            this.bind = this.scroll.bind(this);
            window.addEvent('scroll', this.bind);
            window.fireEvent('scroll');
        },
        scroll: function () {
            if (this.hasFired) return;
            var bottom = window.getScroll().y + window.getSize().y;
            if (bottom - this.options.topMinus > this.coords.top) {
                this.section.fireEvent('interact').removeEvents('interact');
                this.hasFired = true;
            }
        }
    });
    Extensions.SignupForm = new Class({
        form: undefined,
        initialize: function (form) {
            this.form = form;
            this.form.addEvent('submit', this.send.bind(this));
        },
        send: function () {
            new Request.JSON({
                url: this.form.get('action'),
                method: this.form.get('method'),
                data: this.form.toQueryString(),
                onSuccess: this.success.bind(this),
                onError: this.error.bind(this),
                onFailure: this.error.bind(this)
            }).send();
            return false;
        },
        success: function (json) {
            if (json.success) {
                Extensions.hexalert('Thanks!', true, '')
            } else {
                Extensions.hexalert('Sorry, we didn\'t catch your email.', false, 'secondary')
            }
        },
        error: function () {
            Extensions.hexalert('Whoops! Something went really wrong!', false, 'secondary')
        }
    });
    Extensions.Placeholder = new Class({
        initialize: function (input) {
            this.input = input;
            this.isVisible = !this.input.get('value');
            this.inputWrap = new Element('div', {
                'class': 'placeholder-wrap'
            });
            this.placeholder = new Element('span', {
                text: this.input.get('placeholder'),
                'class': 'placeholder',
                styles: Object.merge(this.input.getStyles('line-height', 'padding'), {
                    display: this.isVisible ? 'block' : 'none'
                })
            });
            this.inputWrap.wraps(this.input).grab(this.placeholder);
            var check = this.checkPlaceholder.bind(this);
            this.input.addEvents({
                propertychange: check,
                input: check,
                change: check,
                keyup: check,
                blur: check
            });
        },
        checkPlaceholder: function () {
            if (this.isVisible && this.input.get('value')) {
                this.placeholder.setStyle('display', 'none');
                this.isVisible = false;
            } else if (!this.isVisible && !this.input.get('value')) {
                this.placeholder.setStyle('display', 'block');
                this.isVisible = true;
            }
        }
    });
    Extensions.instantiate = function (toMake) {
        toMake.each(function (obj) {
            document.getElements(obj.sel).each(function (el) {
                Extensions.Inst[obj.ext] = new Extensions[obj.ext](el, typeof obj.opt === 'undefined' ? {} : obj.opt);
            });
        });
    };
    Extensions.HexAlertBox = new Class({
        hex: undefined,
        overlay: undefined,
        hide_event: undefined,
        initialize: function () {
            this.create_hex();
            this.hide_event = this.hide.bind(this);
        },
        create_hex: function () {
            var span = new Element('a');
            this.inner = new Element('div').addClass('inner');
            this.hex = new Element('div').addClass('list-hex-grid large clearfix hexalert');
            this.hex.grab(new Element('div').addClass('hex grid-4 primary').setStyle('margin-top', 0).grab(span));
            span.grab(this.inner);
            span.grab(new Element('div').addClass('hex-1'))
            span.grab(new Element('div').addClass('hex-2'))
        },
        set: function (attribute, value) {
            this.inner.set(attribute, value);
        },
        attach: function (to) {
            this.overlay = to;
            this.overlay.setStyle('display', 'block');
            this.overlay.set('tween', {
                duration: 400
            }).tween('opacity', 0.1);
            this.overlay.grab(this.hex, 'after');
            this.hex.set('tween', {
                duration: 400
            }).tween('opacity', 1);
            this.hex.setStyles({
                top: (window.getSize().y - this.hex.getSize().y) / 2,
                left: (window.getSize().x - this.hex.getSize().x) / 2,
                marginTop: 0
            });
            window.addEvents({
                click: this.hide_event,
                keydown: this.hide_event
            });
        },
        addClass: function (cssclass) {
            this.inner.getParents('div.hex').shift().addClass(cssclass);
        },
        hide: function (e) {
            if (e.key !== undefined && e.key !== 'esc') return false;
            this.hex.set('tween', {
                duration: 400,
                onComplete: (function () {
                    this.hex.dispose();
                }).bind(this)
            }).tween('opacity', 0);
            this.overlay.set('tween', {
                duration: 400,
                onComplete: (function () {
                    this.overlay.setStyle('display', this.overlay.getStyle('opacity') == 0 ? 'none' : 'block');
                }).bind(this)
            }).tween('opacity', 0);
            window.removeEvents({
                click: this.hide_event,
                keydown: this.hide_event
            });
            return true;
        }
    });
    Extensions.hexalert = function (string, buffalo, cssclass) {
        buffalo = buffalo ? '<img src="/assets/img/hex-img-8@2x.png" width="70" height="76" alt="">' : '';
        cssclass = cssclass || '';
        var string = buffalo + '<p class="h1">' + string + '</p>',
            box = new Extensions.HexAlertBox();
        box.set('html', string);
        box.addClass(cssclass);
        box.attach(document.id('modal-overlay'));
    };
    window.addEvent('domready', function () {
        Extensions.instantiate([{
            sel: '#nav',
            ext: 'Nav'
        }, {
            sel: 'form.custom-form',
            ext: 'CustomForm',
            opt: {
                elements: ['label.custom-radio', 'label.custom-checkbox', 'label.custom-file', 'div.custom-select']
            }
        }, {
            sel: '#to-top',
            ext: 'Topper'
        }, {
            sel: 'ul.slider-regular',
            ext: 'Slider',
            opt: {
                duration: 300
            }
        }, {
            sel: 'div.list-hex-grid',
            ext: 'Hexagons'
        }, {
            sel: '#modal',
            ext: 'Modal'
        }, {
            sel: '[data-interactive]',
            ext: 'Interactive',
            opt: {
                topMinus: 300
            }
        }, {
            sel: '#newsletter-signup',
            ext: 'SignupForm'
        }]);
        if (Modernizr.touch === true) {
            document.id('modal').setStyle('-webkit-backface-visibility', 'hidden');
        }
        if (!Modernizr.input.placeholder) {
            Extensions.instantiate([{
                sel: 'input[placeholder]',
                ext: 'Placeholder'
            }]);
        }
        if (Browser.chrome && Browser.version > 32 && Browser.version < 35) {
            var css = '.list-hex-grid div.hex > a, .list-hex-grid div.hex > div, .list-hex-grid div.hex .hex-1, .list-hex-grid div.hex .hex-2  { -webkit-backface-visibility: visible; }';
            var style = document.createElement('style');
            style.type = 'text/css';
            style.appendChild(document.createTextNode(css));
            document.head.appendChild(style);
        }
    });
})(window, window.Extensions || {});
window.addEvent('domready', function () {
    var div = document.id('ie6');
    if (Browser.ie && Browser.version < 7) {
        if (window.getSize().y > div.getSize().y) {
            div.setStyle('height', window.getSize().y - 70);
        }
    }
});
window.addEvent('domready', function () {
    var form = document.getElement('form[action=/interns/save]');
    if (!form) {
        return;
    }
    form.addEvent('submit', function () {
        var iframe = document.id('interns-ajax');
        if (!iframe) {
            form.set('target', 'interns-ajax');
            iframe = new Element('iframe');
            iframe.addClass('hide');
            iframe.set('id', 'interns-ajax');
            iframe.set('name', 'interns-ajax');
            iframe.setStyles({
                position: 'fixed',
                top: 0,
                left: 0
            });
            iframe.addEvent('load', function () {
                var json = JSON.decode(this.contentWindow.document.body.innerText),
                    scroll;
                form.getElements('.error').removeClass('error');
                if (!json) {
                    return;
                }
                if (json.success) {
                    Extensions.hexalert('Thanks!', true, '')
                } else {
                    Extensions.hexalert('Whoops! Something went really wrong!', false, 'secondary')
                    json.errors.each(function (error) {
                        var field = form.getElement('[name=' + error + ']').addClass('error');
                        if (!scroll) {
                            scroll = field;
                        }
                    });
                    new Fx.Scroll(window, {
                        duration: 200
                    }).toElement(scroll);
                }
            });
            document.getElement('body').grab(iframe);
        }
        return true;
    });
});