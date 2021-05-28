

$(document).ready(() => {

    
    loadProdData();
    function loadProdData() {
        let tr = "";
        $.ajax({
            method: "GET",
            url: "/Products/GetProducts",
            success: (result) => {
                console.log(result);
                for (index in result) {
                    tr +=   `<tr>
                                <td>${result[index].name}</td>
                                <td>${result[index].category}</td>
                                <td>${result[index].price}</td>
                                <td>${result[index].stockQty}</td>
                                <td>
                                    <a href='../Products/Edit?id=${result[index].id}'>Edit</a>
                                    <a href='../Products/Details?id=${result[index].id}'>Detail</a>

                                </td>
                            </tr>`;
                }

                $("#tableBody").html(tr);
            },
            error: (err) => {
                console.log(error);
            }
        })
    }
})