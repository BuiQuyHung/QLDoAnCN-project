import { Component, OnInit } from '@angular/core';
import { NganhService } from '../services/nganh.service';
import { Nganh } from '../models/nganh.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PopupComponent } from '../shared/popup/popup.component';

@Component({
  selector: 'app-nganh',
  standalone: true,
  imports: [CommonModule, FormsModule, PopupComponent],
  templateUrl: './nganh.component.html',
  styleUrl: './nganh.component.css'
})
export class NganhComponent implements OnInit {
  danhSachNganh: Nganh[] = [];
  selectedNganh: Nganh | null = null;
  isAddingNew: boolean = false;
  newNganh: Nganh = { maNganh: '', tenNganh: '', maBoMon: '' }; // Bao gồm cả maBoMon
  isEditing: boolean = false;
  editingNganh: Nganh | null = null;
  errorMessage: string = '';
  successMessage: string = '';
  isViewingDetails: boolean = false;
  boMons: { maBoMon: string, tenBoMon: string }[] = []; // Để hiển thị dropdown Bộ môn

  constructor(private nganhService: NganhService) { }

  ngOnInit(): void {
    this.loadDanhSachNganh();
    this.loadDanhSachBoMon(); // Tải danh sách Bộ môn để chọn
  }

  loadDanhSachNganh(): void {
    this.nganhService.getAllNganh().subscribe({
      next: (data) => {
        this.danhSachNganh = data;
      },
      error: (error) => {
        this.errorMessage = 'Lỗi khi tải danh sách ngành.';
        console.error('Lỗi tải danh sách ngành', error);
      }
    });
  }

  loadDanhSachBoMon(): void {
    // Giả sử bạn có một BoMonService để lấy danh sách bộ môn
    // Cần inject BoMonService và gọi API /api/BoMons
    // Ví dụ:
    // this.boMonService.getAllBoMon().subscribe({
    //   next: (data) => {
    //     this.boMons = data;
    //   },
    //   error: (error) => {
    //     console.error('Lỗi tải danh sách bộ môn', error);
    //   }
    // });
    // Tạm thời gán dữ liệu mẫu nếu BoMonService chưa có
    this.boMons = [{ maBoMon: 'BM01', tenBoMon: 'Công nghệ phần mềm' }, { maBoMon: 'BM02', tenBoMon: 'Hệ thống thông tin' }];
  }

  hienThiChiTiet(nganh: Nganh): void {
    this.selectedNganh = nganh;
    this.isViewingDetails = true;
  }

  batDauThemMoi(): void {
    this.isAddingNew = true;
    this.newNganh = { maNganh: '', tenNganh: '', maBoMon: '' };
  }

  batDauChinhSua(nganh: Nganh): void {
    this.editingNganh = { ...nganh };
    this.isEditing = true;
  }

  dongPopupChiTiet(): void {
    this.isViewingDetails = false;
    this.selectedNganh = null;
  }

  dongPopupThemMoi(): void {
    this.isAddingNew = false;
  }

  dongPopupChinhSua(): void {
    this.isEditing = false;
    this.editingNganh = null;
  }

  themNganh(): void {
    this.nganhService.addNganh(this.newNganh).subscribe({
      next: (response) => {
        this.successMessage = 'Thêm ngành thành công.';
        this.loadDanhSachNganh();
        this.isAddingNew = false;
        setTimeout(() => this.successMessage = '', 3000);
      },
      error: (error) => {
        this.errorMessage = 'Lỗi khi thêm ngành.';
        console.error('Lỗi thêm ngành', error);
      }
    });
  }

  capNhatNganh(): void {
    if (this.editingNganh) {
      this.nganhService.updateNganh(this.editingNganh.maNganh, this.editingNganh).subscribe({
        next: (response) => {
          this.successMessage = 'Cập nhật ngành thành công.';
          this.loadDanhSachNganh();
          this.isEditing = false;
          this.editingNganh = null;
          setTimeout(() => this.successMessage = '', 3000);
        },
        error: (error) => {
          this.errorMessage = 'Lỗi khi cập nhật ngành.';
          console.error('Lỗi cập nhật ngành', error);
        }
      });
    }
  }

  huyThemMoi(): void {
    this.isAddingNew = false;
  }

  huyChinhSua(): void {
    this.isEditing = false;
    this.editingNganh = null;
  }

  xoaNganh(maNganh: string): void {
    if (confirm('Bạn có chắc chắn muốn xóa ngành này?')) {
      this.nganhService.deleteNganh(maNganh).subscribe({
        next: (response) => {
          this.successMessage = 'Xóa ngành thành công.';
          this.loadDanhSachNganh();
          this.selectedNganh = null;
          setTimeout(() => this.successMessage = '', 3000);
        },
        error: (error) => {
          this.errorMessage = 'Lỗi khi xóa ngành.';
          console.error('Lỗi xóa ngành', error);
        }
      });
    }
  }
}