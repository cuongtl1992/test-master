# PRODUCT REQUIREMENTS DOCUMENT

## TestMaster: Hệ thống Quản lý Test Case Tập trung với AI

**Phiên bản:** 1.0  
**Ngày:** 09/05/2025

---

## 1. TỔNG QUAN SẢN PHẨM

### 1.1 Mục đích

TestMaster là một hệ thống quản lý test case tập trung, lấy cảm hứng từ TestLink, đồng thời tích hợp khả năng tạo test case tự động từ tài liệu SRS (Software Requirements Specification) bằng AI. Sản phẩm nhằm mục đích tối ưu hóa quy trình kiểm thử phần mềm, giảm công sức tạo test case thủ công và nâng cao chất lượng kiểm thử.

### 1.2 Phạm vi

Hệ thống sẽ hỗ trợ toàn bộ vòng đời kiểm thử phần mềm từ lập kế hoạch, thiết kế test case, thực thi kiểm thử đến báo cáo kết quả, với khả năng tích hợp với các công cụ quản lý lỗi. Điểm khác biệt chính là tích hợp với các AI Agent và LLM để phân tích tài liệu SRS và tạo test case tự động, cùng với khả năng quản lý test plan theo mô hình phân cấp cho môi trường Agile.

### 1.3 Đối tượng người dùng

- **Test Manager/Leader:** Quản lý kế hoạch kiểm thử, phân công công việc
- **Test Engineer:** Tạo và thực thi test case
- **Developer:** Xem kết quả kiểm thử và cập nhật trạng thái lỗi
- **Product Manager:** Theo dõi tiến độ kiểm thử và chất lượng sản phẩm
- **Stakeholders:** Xem báo cáo và chỉ số chất lượng

## 2. YÊU CẦU CHỨC NĂNG CỐT LÕI (MVP)

### 2.1 Quản lý Test Project

- **PRF-01:** Tạo, chỉnh sửa và xóa Test Project
- **PRF-02:** Phân quyền người dùng theo Test Project
- **PRF-03:** Hiển thị tổng quan trạng thái kiểm thử theo Project

### 2.2 Quản lý Test Suite và Test Case

- **PRF-04:** Tạo cấu trúc Test Suite phân cấp
- **PRF-05:** Tạo, sửa, xóa Test Case với các thành phần: Tiêu đề, Mô tả, Các bước thực hiện, Kết quả mong đợi
- **PRF-06:** Nhập/Xuất Test Case (XML, CSV)
- **PRF-07:** Tìm kiếm và lọc Test Case

### 2.3 Quản lý Test Plan Phân cấp

- **PRF-08:** Tạo và quản lý cấu trúc phân cấp Test Plan:
  - **Master Sprint Plan**: Tổng hợp tất cả các test plan trong một sprint
  - **Feature Test Plan**: Test plan cho một tính năng cụ thể
  - **Story Test Plan**: Test plan cho một user story cụ thể
  - **Release Test Plan**: Tổng hợp từ nhiều Sprint Master Plans cho một release

- **PRF-09:** Liên kết giữa các cấp Test Plan:
  - Cho phép liên kết Story Test Plan vào Feature Test Plan
  - Tự động tổng hợp Feature Test Plan vào Master Sprint Plan
  - Tạo Release Test Plan từ nhiều Master Sprint Plan

- **PRF-10:** Tạo các Build trong từng cấp Test Plan
- **PRF-11:** Tự động tính toán và cập nhật trạng thái kiểm thử giữa các cấp
- **PRF-12:** Dashboard tổng quan cho từng cấp Test Plan 
- **PRF-13:** Phân công Test Case cho tester theo từng cấp Test Plan

### 2.4 Thực thi Kiểm thử

- **PRF-14:** Thực thi Test Case và ghi nhận kết quả (Pass/Fail/Blocked)
- **PRF-15:** Thêm ghi chú và đính kèm ảnh chụp màn hình khi thực thi
- **PRF-16:** Tự động cập nhật kết quả thực thi lên các Test Plan cấp cao hơn

### 2.5 Báo cáo

- **PRF-17:** Báo cáo tổng quan tiến độ theo từng cấp Test Plan
- **PRF-18:** Báo cáo tổng hợp toàn bộ Sprint/Release
- **PRF-19:** Báo cáo chi tiết kết quả kiểm thử
- **PRF-20:** Xuất báo cáo (PDF, HTML, Excel)

### 2.6 Quản lý người dùng

- **PRF-21:** Đăng ký, đăng nhập và quản lý tài khoản
- **PRF-22:** Phân quyền theo vai trò (Admin, Test Manager, Tester)
- **PRF-23:** Theo dõi hoạt động người dùng

### 2.7 Tính năng AI: Tạo Test Case từ SRS (Tích hợp LLM)

- **PRF-24:** Tải lên tài liệu SRS (hỗ trợ định dạng DOCX, PDF, Markdown)
- **PRF-25:** Tích hợp với các LLM thông qua API (OpenAI GPT, Anthropic Claude, v.v.)
- **PRF-26:** Xử lý tài liệu SRS thành đoạn văn bản phù hợp để gửi đến LLM
- **PRF-27:** Sử dụng prompt engineering để hướng dẫn LLM tạo test case chất lượng cao
- **PRF-28:** Cho phép người dùng chọn LLM và điều chỉnh cài đặt
- **PRF-29:** Chuyển đổi kết quả từ LLM thành test case có cấu trúc trong hệ thống
- **PRF-30:** Liên kết Test Case với requirement nguồn

## 3. YÊU CẦU CHỨC NĂNG MỞ RỘNG (PHIÊN BẢN ĐẦY ĐỦ)

### 3.1 Quản lý Requirement

- **PRF-31:** Tạo, quản lý requirement và liên kết với Test Case
- **PRF-32:** Phân tích độ bao phủ kiểm thử theo requirement
- **PRF-33:** Theo dõi tác động của thay đổi requirement

### 3.2 Phiên bản hóa Test Case

- **PRF-34:** Quản lý phiên bản của Test Case
- **PRF-35:** So sánh giữa các phiên bản Test Case
- **PRF-36:** Lịch sử thay đổi Test Case

### 3.3 Tích hợp với công cụ bên ngoài

- **PRF-37:** Tích hợp với Jira, Mantis, Bugzilla, Azure DevOps
- **PRF-38:** Đồng bộ hóa lỗi giữa TestMaster và Bug Tracking System
- **PRF-39:** API để tích hợp với CI/CD pipeline
- **PRF-40:** Tự động cập nhật kết quả kiểm thử từ công cụ tự động

### 3.4 Nâng cao khả năng tích hợp AI

- **PRF-41:** Tích hợp với AI Agents chuyên biệt cho kiểm thử (không cần fine-tuning)
- **PRF-42:** Sử dụng LLM để phân tích lỗi và đề xuất giải pháp
- **PRF-43:** Tạo script kiểm thử tự động từ test case thông qua LLM
- **PRF-44:** Đề xuất cải tiến cho test case hiện có dựa trên phản hồi từ AI
- **PRF-45:** Xử lý ngôn ngữ tự nhiên để tìm kiếm và phân loại test case thông minh

### 3.5 Quản lý quy trình kiểm thử

- **PRF-46:** Quy trình phê duyệt Test Case
- **PRF-47:** Quản lý chu kỳ kiểm thử và mốc thời gian
- **PRF-48:** Dashboard tùy chỉnh theo vai trò
- **PRF-49:** Dự báo tiến độ và cảnh báo rủi ro trong Sprint/Release

### 3.6 Tính năng nâng cao cho Test Plan Phân cấp

- **PRF-50:** Phân tích ảnh hưởng khi thay đổi Test Plan
- **PRF-51:** Lập lịch tự động cho Test Plan dựa trên sprint timeline
- **PRF-52:** Báo cáo so sánh giữa các Master Plan của các sprint
- **PRF-53:** Tích hợp Master Plan với công cụ quản lý dự án (Jira, Azure DevOps)
- **PRF-54:** Template Test Plan cho các sprint/release tiếp theo

## 4. YÊU CẦU PHI CHỨC NĂNG

### 4.1 Hiệu suất

- **PRNF-01:** Thời gian phản hồi trang web < 2 giây
- **PRNF-02:** Hỗ trợ đồng thời 100 người dùng
- **PRNF-03:** Thời gian phân tích SRS và tạo Test Case < 5 phút cho tài liệu 50 trang
- **PRNF-04:** Thời gian tổng hợp báo cáo Master Plan < 3 giây cho 500 test cases

### 4.2 Bảo mật

- **PRNF-05:** Mã hóa dữ liệu nhạy cảm
- **PRNF-06:** Xác thực hai yếu tố
- **PRNF-07:** Audit log cho mọi hoạt động quan trọng
- **PRNF-08:** Bảo mật dữ liệu khi tích hợp với LLM, không gửi dữ liệu nhạy cảm

### 4.3 Khả năng sử dụng

- **PRNF-09:** Giao diện thân thiện với người dùng, responsive
- **PRNF-10:** Thời gian học sử dụng < 4 giờ cho người dùng mới
- **PRNF-11:** Hỗ trợ đa ngôn ngữ (tiếng Anh, Việt, và các ngôn ngữ khác)

### 4.4 Độ tin cậy

- **PRNF-12:** Uptime > 99.5%
- **PRNF-13:** Backup dữ liệu hàng ngày
- **PRNF-14:** Khả năng phục hồi sau sự cố < 2 giờ

### 4.5 Khả năng mở rộng

- **PRNF-15:** Kiến trúc cho phép mở rộng theo chiều ngang
- **PRNF-16:** Hỗ trợ quản lý 10,000+ test case
- **PRNF-17:** Khả năng tích hợp dễ dàng với các LLM mới

## 5. ĐẶC ĐIỂM KỸ THUẬT

### 5.1 Kiến trúc hệ thống

- **Kiến trúc Microservices:**
  - Test Management Service
  - User Management Service
  - Execution Service
  - Reporting Service
  - AI Integration Service
  - Test Plan Hierarchy Service

### 5.2 Stack công nghệ đề xuất

- **Frontend:** Angular 19 với TypeScript
  - Angular Material hoặc PrimeNG cho UI components
  - NgRx cho state management
  - RxJS cho xử lý bất đồng bộ
  - D3.js/Chart.js cho visualization dashboard
  
- **Backend:** C# .NET Core
  - ASP.NET Core Web API
  - Entity Framework Core cho ORM
  - SignalR cho real-time communications
  - Hangfire cho background processing
  - MediatR cho CQRS pattern

- **Database:**
  - PostgreSQL cho lưu trữ chính
  - Redis cho caching và phiên làm việc
  
- **AI Integration:**
  - RESTful API kết nối với các LLM providers (OpenAI, Anthropic)
  - .NET SDK cho các dịch vụ AI
  
- **DevOps:**
  - Azure DevOps/GitHub Actions cho CI/CD
  - Docker cho containerization
  - Kubernetes cho orchestration
  
- **Monitoring & Logging:**
  - Application Insights
  - Serilog/NLog
  - Grafana/Prometheus

### 5.3 Tích hợp AI

- **INT-AI-01:** Tích hợp API với OpenAI GPT-4 và các phiên bản mới hơn
- **INT-AI-02:** Tích hợp API với Anthropic Claude và các phiên bản mới hơn
- **INT-AI-03:** Hỗ trợ tích hợp với các LLM mã nguồn mở như Llama, Mistral
- **INT-AI-04:** Kho prompt templates được tối ưu hóa cho việc tạo test case
- **INT-AI-05:** Cơ chế xử lý document chunking để xử lý tài liệu SRS dài
- **INT-AI-06:** Hệ thống cache để tối ưu hóa việc sử dụng API LLM

## 6. THIẾT KẾ DATABASE SƠ BỘ

### 6.1 Các entity chính

- **Users:** Quản lý người dùng và phân quyền
- **Projects:** Thông tin Test Project
- **TestSuites:** Cấu trúc phân cấp Test Suite
- **TestCases:** Chi tiết các Test Case
- **Requirements:** Quản lý requirement
- **TestPlans:** Quản lý Test Plan các cấp
- **TestPlanHierarchy:** Quản lý quan hệ cha-con giữa các Test Plan
- **Builds:** Các phiên bản cần kiểm thử
- **Executions:** Kết quả thực thi Test Case
- **AIConfigs:** Cấu hình kết nối với LLM
- **AIHistory:** Lịch sử tương tác với LLM và kết quả
- **Sprints:** Thông tin về sprint
- **Releases:** Thông tin về release

### 6.2 Quan hệ Database

- PostgreSQL sẽ được dùng cho tất cả dữ liệu có cấu trúc với quan hệ phức tạp
- Redis sẽ được sử dụng cho:
  - Caching truy vấn thường xuyên
  - Session management
  - Lưu trữ tạm thời kết quả AI trước khi xử lý
  - Hàng đợi xử lý tài liệu SRS quy mô lớn
  - Caching cho dashboard Master Plan và Release Plan

## 7. SƠ ĐỒ LUỒNG CHÍNH

### 7.1 Luồng Test Plan Phân cấp

```
1. Tạo Sprint Test Master Plan
   ├── Liên kết với Sprint trong Agile tool
   ├── Xác định phạm vi và mục tiêu kiểm thử
   └── Thiết lập metrics và KPIs

2. Tạo Feature Test Plans
   ├── Tạo Test Plan cho từng tính năng trong sprint
   ├── Liên kết với Master Plan
   ├── Phân công người chịu trách nhiệm
   └── Xác định mức độ ưu tiên

3. Tạo Story Test Plans
   ├── Tạo Test Plan chi tiết cho từng User Story
   ├── Liên kết với Feature Test Plan
   ├── Thêm Test Cases cụ thể
   └── Phân công người thực hiện

4. Thực thi Test Cases
   ├── Thực hiện test trên từng Story Test Plan
   ├── Ghi nhận kết quả
   └── Cập nhật trạng thái

5. Tổng hợp và theo dõi
   ├── Tự động cập nhật kết quả lên cấp cao hơn
   ├── Dashboard tổng thể theo tiến độ
   └── Báo cáo theo từng cấp và tổng thể

6. Release Test Plan
   ├── Tạo Release Test Plan từ các Sprint Master Plans
   ├── Tổng hợp kết quả cuối cùng
   └── Báo cáo tổng thể cho toàn bộ release
```

### 7.2 Luồng AI phân tích SRS

```
Tải SRS → Tiền xử lý tài liệu → Chia tài liệu thành các phần → 
Gửi từng phần đến LLM với prompt phù hợp → 
Nhận kết quả từ LLM → Cấu trúc lại thành test case → 
Hiển thị cho người dùng xem xét → Điều chỉnh nếu cần → 
Lưu vào hệ thống
```

## 8. KẾ HOẠCH TRIỂN KHAI

### 8.1 Phiên bản MVP (2-3 tháng)

- **Milestone 1:** Thiết lập cơ sở hạ tầng và framework (2 tuần)
  - Thiết lập dự án Angular 19 và .NET Core
  - Thiết lập cơ sở dữ liệu PostgreSQL và Redis
  - Thiết lập CI/CD pipeline

- **Milestone 2:** Quản lý Test Case và Test Suite cơ bản (3 tuần)
  - Phát triển backend API cho Test Case và Test Suite
  - Phát triển giao diện người dùng với Angular

- **Milestone 3:** Quản lý Test Plan phân cấp (3 tuần)
  - Thiết kế và phát triển cấu trúc phân cấp Test Plan
  - Phát triển dashboard tổng hợp
  - Phát triển cơ chế liên kết và tổng hợp thông tin

- **Milestone 4:** Tích hợp với LLM và thiết lập prompt (3 tuần)
  - Phát triển service tích hợp với LLM API
  - Triển khai chunking và xử lý tài liệu
  - Thiết lập prompt templates

- **Milestone 5:** Báo cáo cơ bản và hoàn thiện MVP (2 tuần)
  - Phát triển các báo cáo theo cấp Test Plan
  - Thử nghiệm và tối ưu hóa

### 8.2 Phiên bản đầy đủ (6-9 tháng sau MVP)

- **Phase 1:** Tính năng mở rộng và tối ưu hóa (3 tháng)
- **Phase 2:** Tích hợp với hệ thống bên ngoài (2 tháng)
- **Phase 3:** Mở rộng tích hợp AI và thêm AI Agents (2 tháng)
- **Phase 4:** Hoàn thiện và triển khai Enterprise (2 tháng)

## 9. ƯU TIÊN PHÁT TRIỂN

### 9.1 MoSCoW Analysis

**Must Have (MVP):**
- Quản lý Test Case và Test Suite
- Cấu trúc phân cấp Test Plan (Sprint/Feature/Story)
- Thực thi Test Case và ghi nhận kết quả
- Dashboard tổng hợp cho Sprint Master Plan
- Tích hợp cơ bản với một LLM (như GPT-4 hoặc Claude)

**Should Have (Early Post-MVP):**
- Phiên bản hóa Test Case
- Release Test Plan và báo cáo
- Tích hợp với 1-2 công cụ quản lý lỗi phổ biến
- Mở rộng tích hợp với nhiều LLM khác nhau

**Could Have (Later Phases):**
- Tích hợp với CI/CD
- Dashboard tùy chỉnh
- Sử dụng AI để phân tích kết quả kiểm thử

**Won't Have (Version 1.0):**
- Tích hợp với tất cả công cụ bug tracking
- Phân tích mã nguồn tự động
- Dự đoán thời gian và công sức kiểm thử

## 10. METRIC ĐO LƯỜNG THÀNH CÔNG

- **Metric 1:** Độ chính xác của Test Case được tạo bởi LLM (> 80%)
- **Metric 2:** Thời gian tiết kiệm trong việc tạo Test Case (> 60%)
- **Metric 3:** Tỉ lệ lỗi được phát hiện bởi Test Case tự động (so với thủ công)
- **Metric 4:** Mức độ hài lòng của người dùng (> 4/5)
- **Metric 5:** Giảm thời gian tổng hợp báo cáo sprint (> 70%)
- **Metric 6:** Tăng khả năng theo dõi tiến độ kiểm thử trong sprint (> 50%)

---

## PHỤ LỤC

### A. Ví dụ cấu trúc phân cấp Test Plan

**Sprint Master Plan: "Sprint 23 - May 2025"**
- Mục tiêu: Kiểm thử toàn bộ tính năng phát triển trong Sprint 23
- Thời gian: 01/05/2025 - 15/05/2025
- Owner: Jane Doe (Test Manager)
- Test Metrics: 
  - Tổng số test cases: 120
  - Đã thực hiện: 80 (67%)
  - Pass: 65 (81%)
  - Fail: 10 (13%)
  - Blocked: 5 (6%)

**Feature Test Plan: "User Authentication Upgrade"**
- Thuộc Master Plan: "Sprint 23 - May 2025"
- Owner: John Smith (Senior QA)
- Test Metrics:
  - Tổng số test cases: 45
  - Đã thực hiện: 40 (89%)
  - Pass: 32 (80%)
  - Fail: 5 (13%)
  - Blocked: 3 (7%)

**Story Test Plan: "Implement 2FA for Login"**
- Thuộc Feature Plan: "User Authentication Upgrade"
- Owner: Alice Johnson (QA Engineer)
- Test Cases:
  - TC-001: Verify SMS verification code
  - TC-002: Verify email verification code
  - TC-003: Verify 2FA timeout
  - ...

**Release Test Plan: "v2.5 - Q2 2025"**
- Bao gồm các Sprint Master Plans:
  - Sprint 22 (April 2025)
  - Sprint 23 (May 2025)
  - Sprint 24 (June 2025)
- Regression Test Plan
- UAT Test Plan

### B. Ví dụ Prompt cho LLM

**Prompt cho phân tích requirement từ SRS:**
```
Hãy phân tích đoạn tài liệu SRS sau và xác định tất cả các yêu cầu chức năng: 

[Nội dung SRS]

Hãy liệt kê từng yêu cầu chức năng riêng biệt theo định dạng sau:
ID: [Số thứ tự]
Mô tả: [Mô tả ngắn gọn]
Chi tiết: [Chi tiết yêu cầu]
Loại: [Chức năng/Phi chức năng]
```

**Prompt cho tạo Test Case:**
```
Dựa trên yêu cầu sau, hãy tạo các test case đầy đủ:

[Yêu cầu]

Hãy tạo test case theo định dạng:
ID: TC-[số]
Tiêu đề: [Tiêu đề ngắn gọn mô tả mục đích kiểm thử]
Mô tả: [Mô tả chi tiết mục đích kiểm thử]
Điều kiện tiên quyết: [Điều kiện cần có trước khi thực hiện]
Các bước thực hiện:
1. [Bước 1]
2. [Bước 2]
...
Kết quả mong đợi: [Kết quả mong đợi cụ thể]
Mức độ ưu tiên: [Cao/Trung bình/Thấp]

Hãy bao gồm các test case cho trường hợp tích cực, tiêu cực và giá trị biên.
```

### C. Mockup Dashboard Sprint Master Plan

*(Sẽ được cung cấp trong tài liệu thiết kế UI/UX)*

Bao gồm:
- Tổng quan tiến độ kiểm thử
- Biểu đồ phân phối pass/fail theo feature
- Biểu đồ xu hướng theo thời gian
- Danh sách các Feature Test Plans với trạng thái
- Báo cáo tóm tắt và cảnh báo rủi ro

---

*Tài liệu này sẽ được cập nhật dựa trên phản hồi từ stakeholders và sự phát triển của dự án.*