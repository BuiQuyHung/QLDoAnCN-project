import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router'; 

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive, RouterOutlet], 
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent {
  menuItems = [
    { label: 'Quản Lý Khoa', path: '/dashboard/khoa' }, 
    { label: 'Quản Lý Bộ Môn', path: '/dashboard/bo-mon' }, 
    { label: 'Quản Lý Ngành', path: '/dashboard/nganh' },
    { label: 'Quản Lý Chuyên Ngành', path: '/dashboard/chuyennganh' },
    { label: 'Quản Lý Lớp', path: '/dashboard/lop' },
    { label: 'Quản Lý Sinh Viên', path: '/dashboard/sinhvien' },
    { label: 'Quản Lý Giảng Viên', path: '/dashboard/giangvien' },
    { label: 'Quản Lý Đề Tài', path: '/dashboard/detai' },
  ];
}