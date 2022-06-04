const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]');

const tooltipList = [...tooltipTriggerList].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl));

const apiUrl = `https://kenkatathirddevelop.azurewebsites.net/`;
// Kristoffer localUrl:  https://localhost:7167/;

getCart();
updateSession();
//updateIndexCart();     it is run as updateThisIndexCart directly in view as a script element



function addToCart(event) {

    event.stopPropagation();
    fetch(`${apiUrl}shoppingcart/addtocart/${event.currentTarget.dataset.product}`)
        .then(res => res.text())
        .then(data => {
            localStorage.setItem("shoppingCart", data);
            getCart();

        });
}

function addManyToCart(event) {

    let sendData = [parseInt(event.currentTarget.dataset.product), parseInt(document.querySelector("#detailsQuantity").innerHTML)];

    event.stopPropagation();
    fetch(`${apiUrl}shoppingcart/addmanytocart`,
        {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(sendData)
})
        .then(res => res.text())
        .then(data => {
            localStorage.setItem("shoppingCart", data);
            getCart();
        });
}

function increaseDetailsQuantity() {
    document.querySelector("#detailsQuantity").innerHTML++;
}

function decreaseDetailsQuantity() {
    document.querySelector("#detailsQuantity").innerHTML--;
}


function updateSession() {

    fetch(`${apiUrl}shoppingcart/updateSession`,
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

    if (shoppingCart === null) {
        fetch(`${apiUrl}shoppingcart/addtocart/0`)
            .then(res => res.text())
            .then(data => {
                localStorage.setItem("shoppingCart", data);

                if (shoppingCart.TotalQuantity !== 0) {
                    shoppingCart = JSON.parse(localStorage.getItem("shoppingCart"));
                    document.getElementById('totalQuantity').innerText = shoppingCart.TotalQuantity;
                    document.getElementById('totalQuantity').classList.add('bg-turquoise');
                    document.getElementById('totalPrice').innerText = `${shoppingCart.TotalPrice}:-`;
                } else {
                    document.getElementById('totalQuantity').innerHTML = "";
                    document.getElementById('totalQuantity').classList.remove("bg-turquoise");
                    document.getElementById('totalPrice').innerText = "";
                }
            });
    } else {
        if (shoppingCart.TotalQuantity !== 0) {
            document.getElementById('totalQuantity').innerText = shoppingCart.TotalQuantity;
            document.getElementById('totalQuantity').classList.add("bg-turquoise");
            document.getElementById('totalPrice').innerText = `${shoppingCart.TotalPrice}:-`;
        } else {
            document.getElementById('totalQuantity').innerHTML = "";
            document.getElementById('totalQuantity').classList.remove("bg-turquoise");
            document.getElementById('totalPrice').innerText = "";
        }
    }
}



async function updateIndexCart() {

    let shoppingCart = JSON.parse(localStorage.getItem("shoppingCart"));

    document.querySelector("#cartBody").innerHTML = "";
    document.querySelector("#cartTotalsSubTotal").innerText = "0";

    if (shoppingCart.Items[0] !== undefined) {
        let shoppingCart = JSON.parse(localStorage.getItem("shoppingCart"));

        document.querySelector("#cartBody").innerHTML = "";

        for (let i = 0; i < shoppingCart.Items.length; i++) {
            let productName = shoppingCart.Items[i].Product.Name;
            let productPrice = shoppingCart.Items[i].Product.Price;
            let productQuantity = shoppingCart.Items[i].Quantity;
            let productSubTotal = shoppingCart.Items[i].Product.Price * shoppingCart.Items[i].Quantity;

            document.querySelector("#cartBody").innerHTML +=

                `<tr>
                    <td><i onclick="removeFromCart(${shoppingCart.Items[i].Product.Id})" class="fa-solid fa-circle-xmark text-secondary"></i></td> <td>${productName}</td>
                    <td>${productPrice}</td>
                    <td>
                        <div class="d-flex">
                            <div class="button-minus border-lightgrey bg-white text-secondary h-50 px-2" onclick="subtractOneFromQuantity(event)" data-product="${shoppingCart.Items[i].Product.Id}">-</div>
                        <div class="border-lightgrey px-2 h-50">${productQuantity}</div> 
                         <div class="button-plus bg-turquoise text-white border-turquoise h-50 px-2 " onclick="addOneToQuantity(event)" data-product="${shoppingCart.Items[i].Product.Id}">+</div>
                        </div>
                    </td>
                    <td>${productSubTotal}</td> 
                </tr>`;

        }

        document.querySelector("#cartTotalsSubTotal").innerText = `${shoppingCart.TotalPrice}`;
    }

}

function removeFromCart(id) {

    fetch(`${apiUrl}shoppingcart/deleteitemcart/${id}`)
        .then(res => res.text())
        .then(data => {
            localStorage.setItem("shoppingCart", data);
            
            updateIndexCart();
            getCart();
        });


}

function subtractOneFromQuantity(event) {
    let shoppingCart = JSON.parse(localStorage.getItem("shoppingCart"));

    console.log(event.currentTarget.dataset.product);

    for (let i = 0; i < shoppingCart.Items.length; i++) {
        var ifTest = `${shoppingCart.Items[i].Product.Id}`;
        if (ifTest === event.currentTarget.dataset.product) {
            if (shoppingCart.Items[i].Quantity > 0) {
                shoppingCart.Items[i].Quantity--;
                shoppingCart.TotalQuantity--;
                shoppingCart.TotalPrice = 0;
                if (shoppingCart.Items[i].Quantity === 0) {

                    removeFromCart(shoppingCart.Items[i].Product.Id);
                }
            }
        }
    }

    for (let i = 0; i < shoppingCart.Items.length; i++) {
        if (shoppingCart.Items[i].Quantity > 0) {
            shoppingCart.TotalPrice += shoppingCart.Items[i].Product.Price * shoppingCart.Items[i].Quantity;
        }
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




