import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
// forms
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
// store
import { StoreModule } from '@ngrx/store';
// effects
import { EffectsModule } from '@ngrx/effects';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { ComprarComponent } from './comprar/comprar.component';
import { CotizarComponent } from './cotizar/cotizar.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ComprarComponent,
    CotizarComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    EffectsModule.forRoot([]), // always
    StoreModule.forRoot({}),
    // forms
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'compra', component: ComprarComponent },
      { path: 'cotizaciones', component: CotizarComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
