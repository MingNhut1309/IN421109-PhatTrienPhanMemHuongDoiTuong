# User Stories — Web bán vé xem phim

## Khách hàng

### US-01 · Xem suất chiếu còn ghế trống
Là khách hàng, tôi muốn xem các suất chiếu của một bộ phim cùng số ghế còn trống, để chọn được suất phù hợp trước khi đặt vé.
- Given suất chiếu có 100 ghế và đã bán 40 vé, When tôi xem suất chiếu, Then thấy hiển thị còn 60/100 ghế.
- Given suất chiếu đã bán hết ghế, When tôi xem suất chiếu, Then thấy trạng thái hết vé và không thể chọn đặt.

### US-02 · Đặt vé và thanh toán online
Là khách hàng, tôi muốn chọn ghế, đặt vé và thanh toán trực tuyến, để không phải xếp hàng mua vé tại quầy.
- Given suất chiếu còn ghế, When tôi chọn ghế và đặt vé, Then hệ thống tạo đơn đặt vé ở trạng thái Chờ thanh toán.
- Given đơn đang Chờ thanh toán, When tôi thanh toán thành công, Then đơn chuyển sang Đã thanh toán và tôi nhận được mã vé.
- Given ghế tôi chọn vừa bị người khác đặt trước, When tôi đặt vé, Then hệ thống từ chối và yêu cầu tôi chọn ghế khác.

### US-03 · Hủy vé khi không đi được
Là khách hàng, tôi muốn hủy vé đã đặt khi có việc đột xuất, để được trả lại ghế và hoàn tiền theo quy định.
- Given đơn đang Chờ thanh toán hoặc Đã thanh toán và suất chiếu chưa bắt đầu, When tôi hủy, Then đơn chuyển sang Đã hủy và ghế được mở lại cho người khác.
- Given đơn đã Đã hủy, When tôi hủy lần nữa, Then hệ thống báo lỗi.

## Nhân viên rạp

### US-04 · Tạo suất chiếu mới
Là nhân viên rạp, tôi muốn tạo suất chiếu với phim, phòng chiếu, giờ chiếu, số ghế và giá vé, để khách hàng có thể đặt vé.
- Given tên phim hợp lệ, số ghế > 0 và giá vé > 0, When tôi tạo suất chiếu, Then suất chiếu được gắn với đúng phòng chiếu của rạp.
- Given tên phim để trống hoặc số ghế <= 0, When tôi tạo suất chiếu, Then hệ thống từ chối và báo lý do.

### US-05 · Kiểm tra vé tại cửa rạp
Là nhân viên rạp, tôi muốn quét mã vé của khách khi vào phòng chiếu, để chỉ người có vé hợp lệ mới được vào.
- Given đơn đã thanh toán và vé chưa dùng, When tôi quét mã vé, Then hệ thống ghi nhận lượt vào cửa và đánh dấu vé đã sử dụng.
- Given vé đã dùng rồi hoặc đơn đã hủy, When tôi quét mã vé, Then hệ thống từ chối và hiển thị lý do.

## Admin

### US-06 · Xem báo cáo doanh thu theo phim
Là Admin, tôi muốn xem báo cáo số vé đã bán, số khách đã vào rạp và doanh thu của từng bộ phim, để biết phim nào ăn khách và điều chỉnh lịch chiếu.
- Given phim có 2 đơn đã thanh toán và 1 vé đã quét, When tôi xem báo cáo, Then thấy 2 vé bán ra, 1 lượt vào cửa và tổng doanh thu tương ứng.
- Given phim chưa bán được vé nào, When tôi xem báo cáo, Then thấy các số liệu bằng 0 thay vì báo lỗi.