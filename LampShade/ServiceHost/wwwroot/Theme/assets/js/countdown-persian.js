document.addEventListener("DOMContentLoaded", function () {

    // تشخیص نوع صفحه از کلاس body
    const bodyClass = document.body.classList;

    // اضافه کردن استایل بر اساس نوع صفحه
    const style = document.createElement('style');
    if (bodyClass.contains("page-category")) {
        // صفحه گروه محصول → فلکس و row-reverse
        style.textContent = `
            .product-countdown {
                display: flex;
                flex-direction: row-reverse !important;
            }
        `;
    } else if (bodyClass.contains("page-product")) {
        // صفحه خود محصول → فلکس نباشه
        style.textContent = `
            .product-countdown {
                display: block !important;
            }
        `;
    }
    document.head.appendChild(style);

    // تابع تبدیل عدد انگلیسی -> فارسی
    function toPersianDigits(str) {
        if (!str && str !== 0) return '';
        return String(str).replace(/\d/g, function (d) {
            return ['۰', '۱', '۲', '۳', '۴', '۵', '۶', '۷', '۸', '۹'][d];
        });
    }

    // ترجمه لیبل‌ها
    function translateLabel(txt) {
        if (!txt) return '';
        txt = txt.trim().toLowerCase();
        if (txt === 'days' || txt === 'day') return 'روز';
        if (txt === 'hours' || txt === 'hour') return 'ساعت';
        if (txt === 'mins' || txt === 'min' || txt === 'minutes') return 'دقیقه';
        if (txt === 'secs' || txt === 'sec' || txt === 'seconds') return 'ثانیه';
        return txt;
    }

    // تابعی که برای یک container (= .product-countdown) عناصر فارسی رو می‌سازد و Observer می‌سازد
    function setupPersianForContainer(container) {
        if (!container || container.dataset.persianInit === '1') return;
        container.dataset.persianInit = '1';

        const containerObserver = new MutationObserver(function (mutations) {
            for (const m of mutations) {
                if (m.type === 'childList' && m.addedNodes.length) {
                    initSingleCountdowns(container);
                    break;
                }
            }
        });
        containerObserver.observe(container, { childList: true, subtree: false });

        initSingleCountdowns(container);
    }

    function initSingleCountdowns(container) {
        container.querySelectorAll('.single-countdown').forEach(function (item) {
            if (item.dataset.persianProcessed === '1') return;
            item.dataset.persianProcessed = '1';

            const origTime = item.querySelector('.single-countdown-time');
            const origText = item.querySelector('.single-countdown-text');
            if (!origTime || !origText) return;

            origTime.style.visibility = 'hidden';
            origText.style.visibility = 'hidden';

            const persTime = document.createElement('span');
            persTime.className = 'single-countdown-time persian-time';
            persTime.textContent = toPersianDigits(origTime.textContent);

            const persText = document.createElement('span');
            persText.className = 'single-countdown-text persian-text';
            persText.textContent = translateLabel(origText.textContent);

            if (origTime.nextSibling)
                item.insertBefore(persTime, origTime.nextSibling);
            else
                item.appendChild(persTime);

            if (origText.nextSibling)
                item.insertBefore(persText, origText.nextSibling);
            else
                item.appendChild(persText);

            const mo = new MutationObserver(function () {
                persTime.textContent = toPersianDigits(origTime.textContent);
                persText.textContent = translateLabel(origText.textContent);
            });

            mo.observe(origTime, { characterData: true, childList: true, subtree: true });
            mo.observe(origText, { characterData: true, childList: true, subtree: true });

            const itemObserver = new MutationObserver(function (mutations) {
                for (const m of mutations) {
                    if (m.type === 'childList' && m.addedNodes.length) {
                        setTimeout(function () {
                            const newOrigTime = item.querySelector('.single-countdown-time');
                            const newOrigText = item.querySelector('.single-countdown-text');
                            if (newOrigTime) {
                                newOrigTime.style.visibility = 'hidden';
                                persTime.textContent = toPersianDigits(newOrigTime.textContent);
                                mo.disconnect();
                                mo.observe(newOrigTime, { characterData: true, childList: true, subtree: true });
                            }
                            if (newOrigText) {
                                newOrigText.style.visibility = 'hidden';
                                persText.textContent = translateLabel(newOrigText.textContent);
                            }
                        }, 0);
                        break;
                    }
                }
            });
            itemObserver.observe(item, { childList: true, subtree: false });
        });
    }

    document.querySelectorAll('.product-countdown').forEach(function (c) {
        setupPersianForContainer(c);
    });

    const globalObserver = new MutationObserver(function (mutations) {
        for (const m of mutations) {
            if (m.type === 'childList' && m.addedNodes.length) {
                m.addedNodes.forEach(function (n) {
                    if (n.nodeType === 1) {
                        if (n.matches && n.matches('.product-countdown')) {
                            setupPersianForContainer(n);
                        } else {
                            n.querySelectorAll && n.querySelectorAll('.product-countdown').forEach(setupPersianForContainer);
                        }
                    }
                });
            }
        }
    });
    globalObserver.observe(document.body, { childList: true, subtree: true });

});

