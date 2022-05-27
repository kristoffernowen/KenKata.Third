const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]');

const tooltipList = [...tooltipTriggerList].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl));

function addToCart(event) {

    event.stopPropagation()
    fetch(`https://localhost:7167/shoppingcart/addtocart/${event.currentTarget.dataset.product}`)
        .then(res => res.text())
        .then(data => {
            localStorage.setItem("shoppingCart", data);
            getCart();

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

getCart();

updateIndexCart();

async function updateIndexCart() {

    //   This was test   
    //    let shoppingCart = JSON.parse(localStorage.getItem("shoppingCart"));
    // 
    //
    //    var products = [];
    //    var productIds = [];
    //    const sendProducts = [];
    //    
    //
    //
    //
    //    for (let i = 0; i < shoppingCart.Items.length; i++) {
    //        products.push(shoppingCart.Items[i]);
    //        console.log(shoppingCart.Items[i]);
    //    }
    //
    //    for (let i = 0; i < products.length; i++) {
    //        productIds.push(products[i].Product.Id);
    //        console.log(products[i].Product.Id);
    //
    //        sendProducts.push(products[i]);
    //
    //    }
    //
    //
    //    console.log(sendProducts[1].Quantity);

    //      This was test

    const rawResponse = await fetch("https://localhost:7167/shoppingcart/index",
        {
            method: "POST",
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json"
            },
            body: localStorage.getItem("shoppingCart")
        });


    const content = await rawResponse.JSON;

    //    localStorage.setItem("shoppingCart", content);
    //
    //    getCart();



}

function addQuantity(event) {
    addToCart(event);
    location.reload();
}

function subtractQuantity(event) {
    let shoppingCart = JSON.parse(localStorage.getItem("shoppingCart"));

    let items = [];
    let products = [];

    for (let i = 0; i < shoppingCart.Items.length; i++) {
        items.push(shoppingCart.Items[i]);
    }

    for (let i = 0; i < items.length; i++) {
        console.log(items[i]);
        
        products.push(items[i]);

        
    }
    for (let i = 0; i < products.length; i++) {
        console.log(products[i].Product.Id);

        var testIf = products[i].Product.Id.toString();
        if ( testIf === event.currentTarget.dataset.product) {
            products[i].Quantity--;


            //  This probably decreases the wrong value or must be sent elsewhere



            console.log("if");
        }
        console.log("utanför if");
    }

    localStorage.setItem("shoppingCart", JSON.stringify(shoppingCart)) ;
   
    

    location.reload();
    

}


function removeShoppingCart() {
    localStorage.removeItem("shoppingCart");

    fetch(`https://localhost:7167/shoppingcart/deletesession`)
        .then(res => res.text());

    console.log("shoppingCart should have been removed by now");

    location.reload();
}