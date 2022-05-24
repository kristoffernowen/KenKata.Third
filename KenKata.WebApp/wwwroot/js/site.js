const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]')

const tooltipList = [...tooltipTriggerList].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl))

function addToCart(event) {
    console.log("hej");
    
    event.stopPropagation()
    fetch(`https://localhost:7167/shoppingcart/addtocart/${event.currentTarget.dataset.product}`)
        .then(res => res.text())
        .then(data => {
            localStorage.setItem("shoppingCart", data)
            getCart() 
        })
        
}

function getCart() {

    let shoppingCart = JSON.parse(localStorage.getItem("shoppingCart"))
    if (shoppingCart === undefined) {
        fetch(`https://localhost:7167/shoppingcart/addtocart/0`)
            .then(res => res.text())
            .then(data => {
                localStorage.setItem("shoppingCart", data)

                shoppingCart = JSON.parse(localStorage.getItem("shoppingCart"))
                document.getElementById('totalQuantity').innerText = shoppingCart.TotalQuantity;
                document.getElementById('totalPrice').innerText = shoppingCart.TotalPrice;
            })
    } else {
        document.getElementById('totalQuantity').innerText = shoppingCart.TotalQuantity;
        document.getElementById('totalPrice').innerText = shoppingCart.TotalPrice;
    }
}

getCart()
