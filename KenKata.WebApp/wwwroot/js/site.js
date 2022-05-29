const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]');

const tooltipList = [...tooltipTriggerList].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl));

getCart();
updateSession();
updateIndexCart();


function addToCart(event) {

    let shoppingCart = JSON.parse(localStorage.getItem("shoppingCart"));

    for (let i = 0; i < shoppingCart.Items.length; i++) {
        var ifTest = `${shoppingCart.Items[i].Product.Id}`;
        if (ifTest === event.currentTarget.dataset.product) {
            addOneToQuantity(event);

        } else {
            event.stopPropagation()
            fetch(`https://localhost:7167/shoppingcart/addtocart/${event.currentTarget.dataset.product}`)
                .then(res => res.text())
                .then(data => {
                    localStorage.setItem("shoppingCart", data);
                    getCart();

                });
        }
    }
}

function updateSession() {

    fetch(`https://localhost:7167/shoppingcart/updateSession`,
            {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: localStorage.getItem("shoppingCart")
            })
        .then(res => res.text())
        .then(data => {
        });
}

function getCart() {

    let shoppingCart = JSON.parse(localStorage.getItem("shoppingCart"));

    if (shoppingCart === undefined) {
        fetch(`https://localhost:7167/shoppingcart/addtocart/0`)
            .then(res => res.text())
            .then(data => {
                localStorage.setItem("shoppingCart", data);

                shoppingCart = JSON.parse(localStorage.getItem("shoppingCart"));
                document.getElementById('totalQuantity').innerText = shoppingCart.TotalQuantity;
                document.getElementById('totalPrice').innerText = shoppingCart.TotalPrice;
            });
    } else {
        document.getElementById('totalQuantity').innerText = shoppingCart.TotalQuantity;
        document.getElementById('totalPrice').innerText = shoppingCart.TotalPrice;
    }
}



async function updateIndexCart() {

    let shoppingCart = JSON.parse(localStorage.getItem("shoppingCart"));

    document.querySelector("#cartBody").innerHTML = null;

    for (let i = 0; i < shoppingCart.Items.length; i ++) {
        let productName = shoppingCart.Items[i].Product.Name;
        let productPrice = shoppingCart.Items[i].Product.Price;
        let productQuantity = shoppingCart.Items[i].Quantity;
        let productSubTotal = shoppingCart.Items[i].Product.Price * shoppingCart.Items[i].Quantity;

        document.querySelector("#cartBody").innerHTML +=
            `<tr><td></td> <td>${productName}</td><td>${productPrice}</td><td><button onclick="subtractOneFromQuantity(event)" data-product="${shoppingCart.Items[i].Product.Id}">-</button>${productQuantity}  <button onclick="addOneToQuantity(event)" data-product="${shoppingCart.Items[i].Product.Id}">+</button>
</td><td>${productSubTotal}</td> </tr>`;
    }

}                       



function subtractOneFromQuantity(event) {
    let shoppingCart = JSON.parse(localStorage.getItem("shoppingCart"));

    console.log(event.currentTarget.dataset.product);

    for (let i = 0; i < shoppingCart.Items.length; i++) {
        var ifTest = `${shoppingCart.Items[i].Product.Id}`;
        if (ifTest === event.currentTarget.dataset.product) {
            shoppingCart.Items[i].Quantity--;
            shoppingCart.TotalQuantity--;
            shoppingCart.TotalPrice = 0;
        }
    }
    for (let i = 0; i < shoppingCart.Items.length; i++) {
        shoppingCart.TotalPrice += shoppingCart.Items[i].Product.Price * shoppingCart.Items[i].Quantity;
    }
    localStorage.setItem("shoppingCart", JSON.stringify(shoppingCart));
    updateIndexCart();
    getCart();
}

function addOneToQuantity(event) {
    let shoppingCart = JSON.parse(localStorage.getItem("shoppingCart"));

    for (let i = 0; i < shoppingCart.Items.length; i++) {
        var ifTest = `${shoppingCart.Items[i].Product.Id}`;
        if (ifTest === event.currentTarget.dataset.product) {
            shoppingCart.Items[i].Quantity++;
            shoppingCart.TotalQuantity++;
            shoppingCart.TotalPrice = 0;

        }
    }
    for (let i = 0; i < shoppingCart.Items.length; i++) {
        shoppingCart.TotalPrice += shoppingCart.Items[i].Product.Price * shoppingCart.Items[i].Quantity;
    }
    localStorage.setItem("shoppingCart", JSON.stringify(shoppingCart));
    updateIndexCart();
    getCart();
}


// not yet implemented removeShoppingCart

//function removeShoppingCart() {
//    localStorage.removeItem("shoppingCart");
//
//    fetch(`https://localhost:7167/shoppingcart/deletesession`)
//        .then(res => res.text());
//
//    console.log("shoppingCart should have been removed by now");
//
//    location.reload();
//}

