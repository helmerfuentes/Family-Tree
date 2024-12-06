import { HttpInterceptorFn } from "@angular/common/http";
import { environment } from "../../../environments/environments";

export const HttpRequestInterceptor: HttpInterceptorFn = (req, next) => {
const fullURL =  `${environment.apiUrl}/${req.url}`;
const newRequest = req.clone({ url:fullURL });
return next(newRequest); 
}