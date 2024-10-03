import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { DASH_SERVER_API_BASE_URL, DashServerDisplay, DashServerElement, DashServerPinboard } from "./dash-server";
import { provideHttpClient, withInterceptorsFromDi } from "@angular/common/http";

@NgModule({
    declarations: [],
    imports: [
        CommonModule,
    ],
    providers: [
        { provide: DASH_SERVER_API_BASE_URL, useValue: 'http://172.23.4.74:5051' }, // TODO: load dynamically
        // { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true },
        { provide: DashServerPinboard },
        { provide: DashServerDisplay },
        { provide: DashServerElement },
        provideHttpClient(withInterceptorsFromDi())
    ]
})
export class DashServerModule { }