import { FormsModule } from "@angular/forms";
import { LoginComponent } from "./pages/login.component";
import { NgModule } from "@angular/core";
import { LoginRoutingModule } from "./login-routing.module";

@NgModule({
  declarations: [
    LoginComponent
  ],
  imports: [
    FormsModule,

    LoginRoutingModule
  ],
  exports: [LoginComponent],
})

export class LoginModule { }
