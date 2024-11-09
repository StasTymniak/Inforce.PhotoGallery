import { NgModule } from "@angular/core";
import { AppComponent } from "./app.component";
import { AppRoutingModule } from "./app-routing.module";
import { BrowserModule } from "@angular/platform-browser";
import { ApiModule } from "./api/api.module";
import { LayoutModule } from "./components/layout/layout.module";
import { NotFoundComponent } from "./components/not-found/not-found.component";

@NgModule({
  declarations: [
    AppComponent,
    NotFoundComponent
  ],
  imports: [
    BrowserModule,

    ApiModule,

    LayoutModule,

    AppRoutingModule,

  ],
  bootstrap: [AppComponent],
})

export class AppModule { }
