import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter, Routes } from '@angular/router';
import { provideHttpClient, withFetch } from '@angular/common/http';
import { LoginComponent } from './auth/login/login.component';
import { DashboardComponent } from './dashboard/dashboard.component'; 
import { KhoaComponent } from './khoa/khoa.component';
import { BoMonComponent } from './bomon/bomon.component';
import { NganhComponent } from './nganh/nganh.component';
import { LopComponent } from './lop/lop.component';
import { ChuyenNganhComponent } from './chuyennganh/chuyennganh.component';
import { DashboardHomeComponent } from './DashboardHomeComponent/dashboard-home.component';
import { SinhVienComponent } from './sinhvien/sinhvien.component';
import { GiangVienComponent } from './giangvien/giangvien.component';
import { DeTaiComponent } from './detai/detai.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  {
    path: 'dashboard',
    component: DashboardComponent,
    children: [
      { path: 'khoa', component: KhoaComponent },
      { path: 'bo-mon', component: BoMonComponent },
      { path: 'nganh', component: NganhComponent },
      { path: 'chuyennganh', component: ChuyenNganhComponent },
      { path: 'lop', component: LopComponent },
      { path: 'sinhvien', component: SinhVienComponent },
      { path: 'giangvien', component: GiangVienComponent },
      { path: 'detai', component: DeTaiComponent },
      { path: '', component: DashboardHomeComponent, pathMatch: 'full' }, 
      
    ]
  },
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: '**', redirectTo: '/login' } 
];

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideHttpClient(withFetch()), 
  ],
};