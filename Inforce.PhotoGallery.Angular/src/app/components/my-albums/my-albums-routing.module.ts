import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { MyAlbumsComponent } from "./pages/my-albums.component";

@NgModule({
  imports: [RouterModule.forChild([
    { path: '', component: MyAlbumsComponent },
  ])],
  exports: [RouterModule]
})

export class MyAlbumsRoutingModule { }
