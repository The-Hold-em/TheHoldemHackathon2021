export const separators = {
  STRING: "/n",
  HTML: "<br/>"
}
export class ApiError {
  errors: any[];
  isShow: boolean;
  path: string;

  constructor(errors: any = [], isShow = false, path = "") {
    this.errors = errors;
    this.isShow = isShow;
    this.path = path;
  }
  static getErrors(error: any, separator = separators.STRING) {
    return error.errors.join(separator);
  }
}
export class ApiResponse {
  data: any;
  statusCode: number;
  isSuccessful: boolean;
  method: string;
  error: ApiError;
  constructor(data = null, statusCode = 200, isSuccessful = true, method = "", error = new ApiError()) {
    this.data = data;
    this.statusCode = statusCode;
    this.isSuccessful = isSuccessful;
    this.method = method;
    this.error = error;
  }
  static success(data = null, code = 200, method = "GET") {
    return new ApiResponse(data, code, true, method, undefined);
  }
  static fail(code: number | undefined, method: string | undefined, isShow: boolean | undefined, path: string | undefined, errors: any[] | undefined) {
    return new ApiResponse(null, code, false, method, new ApiError(errors, isShow, path));
  }
}
class CenteralRequest {
  static async request(req: () => any) {
    try {
      var result = await req();
      return CenteralRequest.successResponse(result);
    } catch (err) {
      return CenteralRequest.errorResponse(err);
    }
  }
  static errorResponse(err: any) {
    if (!err.response)
      return ApiResponse.fail(500, err.request.method, false, err.request.path, ["critical error"]);
    var result = err.response;
    if (!result.data.errorData)
      return ApiResponse.fail(result.request.status, result.request.method, false, result.request.path, [result.data.error]);
    return ApiResponse.fail(result.data.statusCode, result.request.method, result.data.errorData.isShow, result.data.errorData.path, [...result.data.errorData.errors]);
  }
  static successResponse(result: any) {
    var exist = Object.keys(result.data).includes('data');
    var data = exist ? result.data.data : result.data;
    var status = exist ? result.data.statusCode : result.request.status;
    return ApiResponse.success(data, status, result.request.method);
  }
}
export default CenteralRequest
