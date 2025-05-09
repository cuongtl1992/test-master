# Chi tiết triển khai dự án TestMaster

Dựa vào mô tả yêu cầu sản phẩm và giải pháp kỹ thuật, tôi sẽ trình bày kế hoạch triển khai chi tiết cho dự án TestMaster, bao gồm các bước thực hiện và các prompt để sử dụng Cursor AI IDE.

## 1. Thiết lập cơ sở hạ tầng và framework

### 1.1 Tạo cấu trúc dự án và solution

**Prompt cho Cursor:**
```
Tạo cấu trúc dự án .NET 8 cho hệ thống TestMaster theo mô hình monolithic với các module riêng biệt: TestManagement, UserManagement, Execution, Reporting, AIIntegration, và TestPlanHierarchy. Cấu trúc bao gồm các dự án sau:
1. TestMaster.Api - Web API chính
2. TestMaster.Core - Các domain models, interfaces và business logic
3. TestMaster.Infrastructure - Repositories, external services và data access
4. TestMaster.Tests - Unit tests và integration tests

Đảm bảo tuân thủ Clean Architecture và mô hình CQRS với MediatR. Tạo các thư mục con trong Core cho từng module nghiệp vụ.
```

### 1.2 Thiết lập cấu trúc database với Entity Framework Core

**Prompt cho Cursor:**
```
Tạo các entity model classes cho TestMaster trong thư mục TestMaster.Core/Entities theo mô hình ERD trong giải pháp kỹ thuật. Bao gồm các entity: User, Project, TestSuite, TestCase, TestCase​Version, Requirement, TestPlan, TestPlanHierarchy, Build, Execution, Attachment, AiConfig, AiHistory, Sprint, và Release.

Sau đó, tạo DbContext class trong TestMaster.Infrastructure/Data với các DbSet cho mỗi entity và cấu hình relationship giữa các entity. Đảm bảo áp dụng các index, constraint và các best practice của Entity Framework Core 8.0.
```

### 1.3 Thiết lập ứng dụng Angular 19 cho frontend

**Prompt cho Cursor:**
```
Tạo cấu trúc ứng dụng Angular 19 cho frontend của TestMaster với các modules sau:
1. Core module - Chứa các services, guards, và interceptors cơ bản
2. Shared module - Chứa các components, directives, và pipes dùng chung
3. Feature modules - Các module tính năng (dashboard, projects, test-suites, test-cases, test-plans, executions, reports, ai-integration)
4. Layout module - Các components layout

Thiết lập routing với lazy loading cho các feature modules, cấu hình NgRx store cho state management, và thiết lập Angular Material hoặc PrimeNG cho UI components.
```

### 1.4 Cấu hình Docker và Docker Compose

**Prompt cho Cursor:**
```
Tạo Dockerfile cho backend .NET 8 và frontend Angular 19, cùng với docker-compose.yml để thiết lập môi trường development. Docker Compose cần bao gồm:
1. SQL Server hoặc PostgreSQL container
2. Redis container cho caching
3. Backend API container
4. Frontend container
5. Network configuration để các containers có thể giao tiếp với nhau

Đảm bảo sử dụng multi-stage builds cho .NET và Angular để tối ưu kích thước images.
```

## 2. Phát triển Core Domain và Entities

### 2.1 Implement các domain models và interfaces

**Prompt cho Cursor:**
```
Implement các domain models và interfaces chính trong TestMaster.Core:

1. Tạo các interfaces như ITestCaseRepository, ITestSuiteRepository, ITestPlanRepository, ... trong thư mục Core/Interfaces
2. Implement các domain services như TestPlanStatusService, TestCaseVersioningService trong Core/Services
3. Tạo các value objects như TestCaseStatus, TestPlanStatus, UserRole trong Core/ValueObjects
4. Implement các domain events như TestCaseCreated, TestExecutionCompleted trong Core/Events

Đảm bảo tuân thủ Domain-Driven Design principles với rich domain models, separation of concerns, và immutability khi cần thiết.
```

### 2.2 Implement CQRS pattern với MediatR

**Prompt cho Cursor:**
```
Triển khai CQRS pattern với MediatR trong TestMaster.Core. Tạo các thư mục sau trong mỗi module:

1. /Commands - Chứa các command và command handlers (CreateTestCase, UpdateTestPlan, ...)
2. /Queries - Chứa các query và query handlers (GetTestCaseById, GetTestPlanHierarchy, ...)
3. /Validators - Chứa các FluentValidation validators cho mỗi command và query

Implement một command và query cụ thể cho mỗi module như CreateTestCaseCommand, GetTestSuiteByIdQuery, ... với đầy đủ validators và handlers.
```

### 2.3 Implement cơ chế logging và exception handling

**Prompt cho Cursor:**
```
Triển khai hệ thống logging và exception handling cho TestMaster:

1. Tạo custom exception types trong Core/Exceptions (NotFoundException, BusinessRuleException, ...)
2. Tạo middleware xử lý global exception trong Api/Middleware
3. Cấu hình Serilog với structured logging format trong Program.cs
4. Implement logging decorator cho MediatR để tự động log tất cả commands và queries
5. Tạo ErrorDetails dto cho API responses với các thông tin lỗi chuẩn hóa

Đảm bảo các exceptions được xử lý thống nhất và trả về response codes phù hợp (404 cho NotFoundException, 400 cho validation errors, ...)
```

## 3. Triển khai Infrastructure và Data Access

### 3.1 Implement các Repository classes

**Prompt cho Cursor:**
```
Implement Repository pattern cho TestMaster.Infrastructure:

1. Tạo BaseRepository class với các phương thức CRUD cơ bản
2. Implement các concrete repository classes như TestCaseRepository, TestSuiteRepository, TestPlanRepository trong Infrastructure/Repositories
3. Thêm paging, sorting và filtering cho các repositories
4. Implement các extension methods cho IQueryable để hỗ trợ paging và filtering
5. Triển khai unit of work pattern để đảm bảo transaction consistency

Đảm bảo sử dụng async/await cho tất cả database operations và implement các indexing strategies để tối ưu hiệu suất.
```

### 3.2 Triển khai caching service với Redis

**Prompt cho Cursor:**
```
Implement caching strategy với Redis cho TestMaster:

1. Tạo ICacheService interface trong Core/Interfaces
2. Implement RedisCacheService trong Infrastructure/Services
3. Thêm các phương thức GetOrCreateAsync, SetAsync, RemoveAsync, và RemoveByPatternAsync
4. Implement caching decorator cho các queries phổ biến
5. Tạo cơ chế cache invalidation khi data thay đổi

Sử dụng ConnectionMultiplexer để kết nối với Redis và đảm bảo caching được sử dụng một cách strategic cho các read-heavy operations.
```

### 3.3 Implement JWT authentication và authorization

**Prompt cho Cursor:**
```
Implement JWT authentication và authorization cho TestMaster:

1. Tạo AuthService trong Infrastructure/Security
2. Implement JWT token generation với claims dựa trên user roles và permissions
3. Tạo refresh token mechanism
4. Implement custom authorization policies và requirements
5. Cấu hình JWT authentication middleware trong Program.cs
6. Implement two-factor authentication với TwoFactorService

Đảm bảo sử dụng secure practices: token expiration, secure storage cho refresh tokens, và validation đầy đủ.
```

## 4. Triển khai các Core Modules

### 4.1 Implement User Management Module

**Prompt cho Cursor:**
```
Implement User Management Module cho TestMaster:

1. Tạo các API controllers: UserController, AuthController
2. Implement các commands: RegisterUser, LoginUser, UpdateUserRole, ResetPassword
3. Implement các queries: GetUserById, GetUsersByProject, GetUserActivity
4. Tạo các DTOs: UserDto, UserRegistrationDto, LoginDto, JwtResponseDto
5. Implement API endpoints cho tất cả CRUD operations
6. Thêm authorization requirements cho mỗi endpoint

Đảm bảo validation đầy đủ cho tất cả inputs, sử dụng mã hóa cho passwords, và implement role-based access control.
```

### 4.2 Implement Test Management Module

**Prompt cho Cursor:**
```
Implement Test Management Module cho TestMaster:

1. Tạo các API controllers: ProjectController, TestSuiteController, TestCaseController
2. Implement các commands: CreateProject, CreateTestSuite, CreateTestCase, UpdateTestCase
3. Implement các queries: GetProjectById, GetTestSuitesByProject, GetTestCasesByTestSuite
4. Tạo các DTOs: ProjectDto, TestSuiteDto, TestCaseDto, TestCaseVersionDto
5. Implement chức năng phiên bản hóa TestCase
6. Implement import/export TestCase với XML và CSV

Đảm bảo test cases có thể được tổ chức theo cấu trúc phân cấp và có thể được phiên bản hóa để track changes.
```

### 4.3 Implement Test Plan Hierarchy Module

**Prompt cho Cursor:**
```
Implement Test Plan Hierarchy Module cho TestMaster:

1. Tạo TestPlanController với endpoints để quản lý các loại Test Plan
2. Implement TestPlanHierarchyService để quản lý cấu trúc phân cấp và relationships
3. Implement TestPlanStatusService để tính toán và cập nhật trạng thái
4. Tạo các commands: CreateTestPlan, LinkTestPlans, AssignTestCasesToPlan
5. Implement các queries: GetTestPlanHierarchy, GetTestPlanStatus, GetTestPlanAssignments
6. Triển khai cơ chế tự động tổng hợp trạng thái giữa các cấp Test Plan

Đảm bảo các Test Plans có thể được liên kết theo đúng cấu trúc phân cấp Master/Feature/Story/Release và trạng thái được cập nhật tự động.
```

### 4.4 Implement Execution Module

**Prompt cho Cursor:**
```
Implement Execution Module cho TestMaster:

1. Tạo ExecutionController với endpoints cho việc thực thi và quản lý kết quả test
2. Implement ExecutionService để xử lý logic thực thi test cases
3. Tạo các commands: ExecuteTestCase, UpdateExecutionResult, AttachScreenshot
4. Implement các queries: GetExecutionById, GetExecutionsByTestPlan, GetExecutionHistory
5. Thiết kế hệ thống lưu trữ attachments (screenshots, logs)
6. Implement cơ chế cập nhật tự động kết quả lên các Test Plan cấp cao hơn

Đảm bảo module hỗ trợ đầy đủ các trạng thái thực thi (Pass/Fail/Blocked) và cho phép đính kèm evidence.
```

### 4.5 Implement Reporting Module

**Prompt cho Cursor:**
```
Implement Reporting Module cho TestMaster:

1. Tạo ReportController với endpoints cho các loại báo cáo
2. Implement ReportingService để tạo các báo cáo động
3. Thiết kế ReportTemplate system cho các loại báo cáo khác nhau
4. Tạo các queries: GetTestPlanSummary, GetSprintReport, GetReleaseReport
5. Implement export functionality cho các định dạng PDF, HTML và Excel
6. Tạo các DTOs cho visualization data (charts, graphs)

Đảm bảo hệ thống báo cáo cung cấp cả high-level dashboards và detailed reports với khả năng drill-down.
```

## 5. Triển khai AI Integration

### 5.1 Implement LLM Provider Integration

**Prompt cho Cursor:**
```
Implement LLM Provider Integration cho TestMaster:

1. Tạo ILlmProvider interface trong Core/Interfaces/AI
2. Implement concrete providers: OpenAiProvider, AnthropicProvider, LlamaProvider
3. Tạo LlmProviderFactory để instantiate providers phù hợp
4. Implement các classes như LlmRequest, LlmResponse, LlmRequestOptions
5. Thiết kế retry mechanism và error handling cho API calls
6. Tích hợp với Azure Key Vault hoặc giải pháp tương tự để quản lý API keys

Đảm bảo mỗi provider implementation tuân thủ interface chung nhưng xử lý các đặc thù riêng của từng LLM API.
```

### 5.2 Implement Document Processing

**Prompt cho Cursor:**
```
Implement Document Processing cho AI Integration trong TestMaster:

1. Tạo DocumentProcessor class để xử lý các định dạng SRS khác nhau (DOCX, PDF, Markdown)
2. Implement document chunking algorithm để chia tài liệu thành các phần phù hợp
3. Tạo các classes như DocumentChunk, ChunkMetadata để lưu trữ thông tin chunks
4. Implement DocumentSanitizer để loại bỏ thông tin nhạy cảm trước khi gửi đến LLM
5. Thiết kế cơ chế xử lý tài liệu dài vượt quá token limits của LLM

Đảm bảo thuật toán chunking thông minh dựa trên cấu trúc tài liệu (headings, sections) thay vì đơn thuần chia theo số tokens.
```

### 5.3 Implement Prompt Engineering

**Prompt cho Cursor:**
```
Implement Prompt Engineering system cho TestMaster:

1. Tạo PromptTemplateRepository để quản lý các mẫu prompt
2. Implement các prompt templates cho phân tích requirement và tạo test case
3. Tạo PromptRenderer service để customize prompts với contextual data
4. Thiết kế PromptEvaluator để đánh giá chất lượng kết quả từ LLM
5. Implement cơ chế để người dùng có thể tùy chỉnh prompt templates

Đảm bảo prompt templates được stored trong database và có thể được version controlled để cải tiến dần dần.
```

### 5.4 Implement Test Case Generation

**Prompt cho Cursor:**
```
Implement Test Case Generation Service cho TestMaster:

1. Tạo TestCaseGenerationService để điều phối quá trình phân tích SRS và tạo test cases
2. Implement requirement extraction logic từ SRS chunks
3. Tạo TestCaseGenerator để chuyển đổi kết quả LLM thành test cases có cấu trúc
4. Implement parsers cho các output formats khác nhau từ LLM
5. Tạo cơ chế liên kết test cases với requirements
6. Thiết kế user interface để xem xét và chỉnh sửa test cases được tạo tự động

Đảm bảo service có thể xử lý tài liệu SRS lớn, với bảo mật dữ liệu và khả năng đánh giá chất lượng test cases tạo ra.
```

## 6. Phát triển Frontend Angular

### 6.1 Implement Authentication và Layout

**Prompt cho Cursor:**
```
Implement Authentication và Layout components cho Angular frontend:

1. Tạo AuthModule với login, register, và reset password components
2. Implement JWT authentication service với interceptor
3. Tạo MainLayout component với responsive sidebar, header và footer
4. Implement các route guards để bảo vệ routes
5. Tạo theme service cho light/dark mode
6. Implement language switching với ngx-translate

Đảm bảo layout responsive trên tất cả device sizes và authentication flow an toàn với refresh token, session timeout và secure storage.
```

### 6.2 Implement Project và Test Suite Management

**Prompt cho Cursor:**
```
Implement Project và Test Suite Management UI cho TestMaster:

1. Tạo ProjectsModule với components cho listing, creating, editing projects
2. Implement TestSuitesModule với tree view cho cấu trúc phân cấp test suites
3. Thiết kế drag-and-drop interface để tổ chức test suites
4. Implement các smart và presentational components tuân thủ container/presentation pattern
5. Tạo các NgRx actions, reducers, effects và selectors cho projects và test suites
6. Implement permission-based UI rendering dựa trên user roles

Đảm bảo interface trực quan, với các visual cues cho trạng thái và interactive tree view cho test suite hierarchy.
```

### 6.3 Implement Test Case Management

**Prompt cho Cursor:**
```
Implement Test Case Management UI cho TestMaster:

1. Tạo TestCaseModule với components cho listing, creating, editing, versioning test cases
2. Implement rich text editor cho test steps và expected results
3. Tạo version comparison view để so sánh giữa các phiên bản test case
4. Implement import/export UI cho test cases
5. Thiết kế search và filter interface
6. Tạo các NgRx actions, reducers, effects và selectors cho test cases

Đảm bảo UI dễ sử dụng với form validation, auto-save drafts, và historical views cho test case versions.
```

### 6.4 Implement Test Plan Hierarchy

**Prompt cho Cursor:**
```
Implement Test Plan Hierarchy UI cho TestMaster:

1. Tạo TestPlanModule với components cho quản lý các loại test plans
2. Implement hierarchical view để hiển thị cấu trúc Master/Feature/Story/Release
3. Thiết kế interface để liên kết giữa các test plans
4. Tạo dashboard visualization cho test plan status với summary cards và progress charts
5. Implement assignment interface để phân công test cases cho testers
6. Tạo các NgRx actions, reducers, effects và selectors cho test plan hierarchy

Đảm bảo UI hiển thị relationships rõ ràng giữa các cấp test plan và tự động cập nhật trạng thái.
```

### 6.5 Implement Execution Interface

**Prompt cho Cursor:**
```
Implement Test Execution UI cho TestMaster:

1. Tạo ExecutionModule với components cho execution workflow
2. Implement step-by-step test execution interface
3. Thiết kế UI để ghi nhận kết quả và đính kèm evidence
4. Tạo interface để view execution history và compare results
5. Implement real-time updates với SignalR cho collaborative testing
6. Tạo các NgRx actions, reducers, effects và selectors cho test execution

Đảm bảo interface tối ưu cho việc thực thi test cases nhanh chóng với keyboard shortcuts và bulk operations.
```

### 6.6 Implement AI Integration UI

**Prompt cho Cursor:**
```
Implement AI Integration UI cho TestMaster:

1. Tạo AiIntegrationModule với components cho tương tác với LLM
2. Implement file upload interface cho tài liệu SRS
3. Thiết kế configuration UI để chọn LLM provider và điều chỉnh các settings
4. Tạo visualization cho quá trình phân tích tài liệu và tạo test cases
5. Implement review và edit interface cho test cases được tạo tự động
6. Tạo dashboard hiển thị metrics về AI performance

Đảm bảo UI trực quan hóa quá trình AI processing và cho phép user control trong từng bước của quá trình.
```

## 7. Triển khai Monitoring và DevOps

### 7.1 Implement Health Checks

**Prompt cho Cursor:**
```
Implement Health Checks cho TestMaster backend:

1. Tạo health checks cho tất cả critical dependencies (database, Redis, LLM services)
2. Implement custom health check cho LlmHealthCheck
3. Tạo HealthController với endpoints cho liveness và readiness probes
4. Cấu hình health checks UI để monitoring health status
5. Implement time-based checks cho các external services
6. Thiết lập alerting khi health checks fail

Đảm bảo health checks cung cấp đủ thông tin để troubleshoot issues nhưng không expose sensitive information.
```

### 7.2 Implement Application Monitoring

**Prompt cho Cursor:**
```
Implement Application Monitoring cho TestMaster:

1. Cấu hình Application Insights cho backend monitoring
2. Implement custom TelemetryInitializer để enrich telemetry data
3. Tạo các custom metrics cho business processes quan trọng
4. Thiết lập performance counters
5. Implement distributed tracing qua các services
6. Cấu hình alerting rules dựa trên performance metrics và error rates

Đảm bảo monitoring bao gồm cả technical metrics (response time, error rate) và business metrics (test case creation rate, execution success rate).
```

### 7.3 Implement CI/CD Pipeline

**Prompt cho Cursor:**
```
Implement CI/CD Pipeline cho TestMaster sử dụng GitHub Actions:

1. Tạo workflow cho CI với build, test và code analysis
2. Implement CD workflow cho automatic deployment
3. Cấu hình database migrations automation
4. Thiết lập environment-specific configurations
5. Implement smoke tests sau deployment
6. Thiết kế blue-green deployment strategy

Đảm bảo pipeline có security scanning, SonarQube integration và automatic rollback khi tests fail.
```

## 8. Rules cho Cursor AI IDE

Dưới đây là bộ rules để đảm bảo Cursor AI IDE implement chính xác từng tác vụ:

```yaml
# TestMaster Project Rules

## Architecture Rules
1. Tuân thủ Clean Architecture với Core, Application, Infrastructure và Presentation layers.
2. Implement CQRS pattern với commands và queries riêng biệt. 
3. Áp dụng Domain-Driven Design cho core business logic.
4. Tất cả database operations phải async/await.
5. Tất cả API controllers phải tuân thủ RESTful design.
6. Các modules phải tách biệt và communicate thông qua well-defined interfaces.

## Coding Standards
1. Sử dụng PascalCase cho classes, interfaces, properties, public methods.
2. Sử dụng camelCase cho variables, parameters và private fields.
3. Thêm XML documentation cho tất cả public APIs.
4. Sử dụng nullable reference types và proper null checking.
5. Group imports theo namespace.
6. Đặt tên rõ ràng, descriptive và theo business concepts.

## Error Handling
1. Tất cả external calls (database, API, filesystem) phải trong try/catch blocks.
2. Sử dụng custom exception types cho business rules.
3. Log tất cả exceptions với context đầy đủ.
4. Không returning exceptions, chỉ return appropriate status codes và error messages.
5. Implement global exception handler.

## Security
1. Sanitize tất cả user inputs.
2. Encrypt sensitive data và không store passwords dưới dạng plain text.
3. Implement proper authentication và authorization.
4. Không expose sensitive information trong logs, error messages hay API responses.
5. Implement Rate Limiting cho tất cả public APIs.

## Performance
1. Implement caching cho read-heavy operations.
2. Sử dụng pagination cho tất cả collection returns.
3. Index tất cả columns được sử dụng trong queries.
4. Tránh N+1 query problem bằng proper eager loading.
5. Minimize số lượng DB round trips.

## Testing
1. Viết unit tests cho tất cả commands và queries.
2. Mock tất cả external dependencies trong unit tests.
3. Implement integration tests cho repositories và API endpoints.
4. Đạt ít nhất 80% code coverage cho core business logic.
5. Sử dụng test doubles (mocks, stubs) một cách appropriate.

## Frontend
1. Tuân thủ container/presentation component pattern.
2. Implement proper state management với NgRx.
3. Lazy load tất cả feature modules.
4. Sử dụng TypeScript interfaces cho tất cả data models.
5. Implement proper form validation.
6. Áp dụng responsive design principles.

## AI Integration
1. Sanitize tất cả nội dung trước khi gửi đến LLM.
2. Implement timeout và retry logic cho LLM API calls.
3. Store LLM API keys trong secure storage (Key Vault).
4. Validate và sanitize LLM responses trước khi processing.
5. Implement fallback mechanisms khi LLM unavailable.

## DevOps
1. Tất cả environment-specific configurations phải trong environment variables.
2. Implement health checks cho tất cả services và dependencies.
3. Containerize application với Docker.
4. Implement automated database migrations.
5. Thiết lập monitoring cho tất cả critical paths.
```

Bằng cách tuân thủ kế hoạch triển khai chi tiết này và các rules cho Cursor AI IDE, bạn sẽ có thể implement dự án TestMaster một cách có hệ thống và đạt được kết quả production-ready.