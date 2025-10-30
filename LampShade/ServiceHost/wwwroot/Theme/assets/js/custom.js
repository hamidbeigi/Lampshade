const cookieName = "cart-items";

// تابع کمکی برای گرفتن محصولات از کوکی
function getProductsFromCookie() {
    let products = $.cookie(cookieName);

    if (!products) {
        return []; // اگر کوکی وجود نداشت یا خالی بود، آرایه‌ی خالی برگردون
    }

    try {
        return JSON.parse(products);
    } catch (e) {
        console.error("خطا در parse کردن کوکی:", e);
        return []; // اگر خراب بود، آرایه‌ی خالی برگردون
    }
}


function addToCart(id, name, price, picture) {
    let products = getProductsFromCookie();

    const count = $("#productCount").val();
    const currentProduct = products.find(x => x.id === id);
    if (currentProduct !== undefined) {
        products.find(x => x.id === id).count = parseInt(currentProduct.count) + parseInt(count);
    } else {
        const product = {
            id,
            name,
            unitPrice: price,
            picture,
            count
        }

        products.push(product);
    }

    $.cookie(cookieName, JSON.stringify(products), { expires: 2, path: "/" });
    updateCart();
}

function updateCart() {
    let products = getProductsFromCookie();

    $("#cart_items_count").text(products.length);
    const cartItemsWrapper = $("#cart_items_wrapper");
    cartItemsWrapper.html('');
    products.forEach(x => {
        const product = `<div class="single-cart-item">
                            <a href="javascript:void(0)" class="remove-icon" onclick="removeFromCart('${x.id}')">
                                <i class="ion-android-close"></i>
                            </a>
                            <div class="image">
                                <a href="single-product.html">
                                    <img src="/images/${x.picture}"
                                         class="img-fluid" alt="">
                                </a>
                            </div>
                            <div class="content">
                                <p class="product-title">
                                    <a href="single-product.html">محصول: ${x.name}</a>
                                </p>
                                <p class="count">تعداد: ${x.count}</p>
                                <p class="count">قیمت واحد: ${Number(x.unitPrice).toLocaleString('en-US')}</p>

                            </div>
                        </div>`;

        cartItemsWrapper.append(product);
    });
}

function removeFromCart(id) {
    let products = getProductsFromCookie();

    const itemToRemove = products.findIndex(x => x.id === id);
    products.splice(itemToRemove, 1);
    $.cookie(cookieName, JSON.stringify(products), { expires: 2, path: "/" });
    updateCart();
}

function changeCartItemCount(id, totalId, count) {

    // 1) اعتبارسنجی و نرمال‌سازی مقدار
    let v = parseInt(count);
    if (isNaN(v) || v < 1) v = 1;

    // 2) اصلاح مقدار در خود UI (اختیاری اما بهتره که هم‌راستا باشه)
    // اگر در inputها data-id گذاشتی، این خط مقدار نمایش‌داده‌شده را هم اصلاح می‌کند
    const input = document.querySelector(`.cart-quantity[data-id="${id}"]`);
    if (input && input.value != v) input.value = v;


    let products = getProductsFromCookie();

    const productIndex = products.findIndex(x => x.id == id);
    products[productIndex].count = v;
    const product = products[productIndex];
    const newPrice = parseInt(product.unitPrice) * v;
    $(`#${totalId}`).text(newPrice.toLocaleString('en-US'));
    products[productIndex].totalPrice = newPrice;
    $.cookie(cookieName, JSON.stringify(products), { expires: 2, path: "/" });
    updateCart();


    //const data = {
    //    'productId': parseInt(id),
    //    'count': parseInt(count)
    //};

    //$.ajax({
    //    url: url,
    //    type: "post",
    //    data: JSON.stringify(data),
    //    contentType: "application/json",
    //    dataType: "json",
    //    success: function (data) {
    //        if (data.isInStock == false) {
    //            const warningsDiv = $('#productStockWarnings');
    //            if ($(`#${id}-${colorId}`).length == 0) {
    //                warningsDiv.append(`<div class="alert alert-warning" id="${id}-${colorId}">
    //                    <i class="fa fa-exclamation-triangle"></i>
    //                    <span>
    //                        <strong>${data.productName} - ${color
    //                    } </strong> در حال حاضر در انبار موجود نیست. <strong>${data.supplyDays
    //                    } روز</strong> زمان برای تامین آن نیاز است. ادامه مراحل به منزله تایید این زمان است.
    //                    </span>
    //                </div>
    //                `);
    //            }
    //        } else {
    //            if ($(`#${id}-${colorId}`).length > 0) {
    //                $(`#${id}-${colorId}`).remove();
    //            }
    //        }
    //    },
    //    error: function (data) {
    //        alert("خطایی رخ داده است. لطفا با مدیر سیستم تماس بگیرید.");
    //    }
    //});


    const settings = {
        "url": "https://localhost:5001/api/inventory",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify({ "productId": id, "count": v })
    };

    $.ajax(settings).done(function (data) {
        if (data.isStock == false) {
            const warningsDiv = $('#productStockWarnings');
            if ($(`#${id}`).length == 0) {
                warningsDiv.append(`
                    <div class="alert alert-warning" id="${id}">
                        <i class="fa fa-warning"></i> کالای
                        <strong>${data.productName}</strong>
                        در انبار کمتر از تعداد درخواستی موجود است.
                    </div>
                `);
            }
        } else {
            if ($(`#${id}`).length > 0) {
                $(`#${id}`).remove();
            }
        }
    });
}