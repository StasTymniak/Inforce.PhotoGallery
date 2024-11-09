import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { LoginComponent } from "./pages/login.component";

@NgModule({
  imports: [RouterModule.forChild([
    { path: '', component: LoginComponent }
  ])],
  exports: [RouterModule]
})

export class LoginRoutingModule { }
