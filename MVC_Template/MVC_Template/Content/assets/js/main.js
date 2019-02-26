function DeleteData(ChoosedId, ChoosedController) {
    let choosedID = ChoosedId;
    //Gelen Controllerın ismine /DeleteAjax eklediğimizde bir controller için bu method bulunduğundan bu değeri url'ye atıyoruz
    let controller ="/"+ ChoosedController + "/DeleteAjax/";
   
    swal({
        title: "Silmek İstediğinize emin misiniz?",
        text: "Bir kere sildiğinde bir daha geri getiremezsiniz!",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                url: controller + choosedID,
                type: 'POST',
                success: function (result) {
                    if (result == true) {
                        /* Eğer dönen değerler içerisinde  Ajax ta yolladığımız url içindeki silinecek id yi var mı
                        diye kontrol ettiğimiz de true ise*/
                        swal("Silme işlemi gerçekleşti", { icon: "success", });
                        setTimeout(function () {
                            location.reload();
                        }, 1000);
                    } else {
                        swal("Silme işlemi yapılamadı Hata Oluştu", { icon: "warning", });
                    }

                }

            });
        }
    });
}