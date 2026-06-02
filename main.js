document.addEventListener('DOMContentLoaded', () => {
  // ==========================================================================
  // 1. MOBILE NAVIGATION & HAMBURGER
  // ==========================================================================
  const menuToggle = document.querySelector('.menu-toggle');
  const mobileNav = document.querySelector('.mobile-nav');

  if (menuToggle && mobileNav) {
    menuToggle.addEventListener('click', () => {
      menuToggle.classList.toggle('open');
      mobileNav.classList.toggle('open');
      
      // Prevent body scrolling when mobile menu is open
      if (mobileNav.classList.contains('open')) {
        document.body.style.overflow = 'hidden';
      } else {
        document.body.style.overflow = 'auto';
      }
    });

    // Close mobile nav when clicking a link
    const mobileLinks = mobileNav.querySelectorAll('a');
    mobileLinks.forEach(link => {
      link.addEventListener('click', () => {
        menuToggle.classList.remove('open');
        mobileNav.classList.remove('open');
        document.body.style.overflow = 'auto';
      });
    });
  }

  // ==========================================================================
  // 2. SCROLL EFFECT ON HEADER (STICKY HEADER)
  // ==========================================================================
  const header = document.querySelector('header');
  
  if (header) {
    const handleScroll = () => {
      if (window.scrollY > 50) {
        header.classList.add('scrolled');
      } else {
        header.classList.remove('scrolled');
      }
    };
    
    // Check scroll position on load
    handleScroll();
    window.addEventListener('scroll', handleScroll);
  }

  // ==========================================================================
  // 3. TESTIMONIALS SLIDER
  // ==========================================================================
  const sliderContainer = document.querySelector('.testimonials-slider');
  
  if (sliderContainer) {
    const slides = sliderContainer.querySelectorAll('.testimonial-slide');
    const prevBtn = document.querySelector('.slider-btn.prev');
    const nextBtn = document.querySelector('.slider-btn.next');
    let currentSlide = 0;
    let slideInterval;

    const showSlide = (index) => {
      // Remove active class from all slides
      slides.forEach(slide => {
        slide.classList.remove('active');
      });

      // Handle wrap-around index
      if (index >= slides.length) {
        currentSlide = 0;
      } else if (index < 0) {
        currentSlide = slides.length - 1;
      } else {
        currentSlide = index;
      }

      // Add active class to current slide
      slides[currentSlide].classList.add('active');
    };

    const nextSlide = () => {
      showSlide(currentSlide + 1);
    };

    const prevSlide = () => {
      showSlide(currentSlide - 1);
    };

    // Auto-advance slides every 6 seconds
    const startAutoPlay = () => {
      slideInterval = setInterval(nextSlide, 6000);
    };

    const stopAutoPlay = () => {
      clearInterval(slideInterval);
    };

    // Event Listeners for controls
    if (nextBtn) {
      nextBtn.addEventListener('click', () => {
        stopAutoPlay();
        nextSlide();
        startAutoPlay();
      });
    }

    if (prevBtn) {
      prevBtn.addEventListener('click', () => {
        stopAutoPlay();
        prevSlide();
        startAutoPlay();
      });
    }

    // Initialize slider
    showSlide(currentSlide);
    startAutoPlay();
  }

  // ==========================================================================
  // 4. PORTFOLIO FILTERING
  // ==========================================================================
  const filterContainer = document.querySelector('.portfolio-filters');
  const portfolioGrid = document.querySelector('.portfolio-grid');

  if (filterContainer && portfolioGrid) {
    const filterBtns = filterContainer.querySelectorAll('.filter-btn');
    const portfolioItems = portfolioGrid.querySelectorAll('.portfolio-item');

    filterBtns.forEach(btn => {
      btn.addEventListener('click', () => {
        // Remove active class from all buttons
        filterBtns.forEach(b => b.classList.remove('active'));
        // Add active class to clicked button
        btn.classList.add('active');

        const filterValue = btn.getAttribute('data-filter');

        portfolioItems.forEach(item => {
          const itemCategory = item.getAttribute('data-category');
          
          if (filterValue === 'all' || itemCategory === filterValue) {
            item.style.display = 'block';
            // Trigger a minor transition animation
            setTimeout(() => {
              item.style.opacity = '1';
              item.style.transform = 'scale(1)';
            }, 50);
          } else {
            item.style.opacity = '0';
            item.style.transform = 'scale(0.95)';
            setTimeout(() => {
              item.style.display = 'none';
            }, 300);
          }
        });
      });
    });
  }

  // ==========================================================================
  // 5. CONTACT FORM VALIDATION
  // ==========================================================================
  const contactForm = document.getElementById('contact-form');
  
  if (contactForm) {
    contactForm.addEventListener('submit', (e) => {
      e.preventDefault();

      const nameInput = document.getElementById('name');
      const emailInput = document.getElementById('email');
      const messageInput = document.getElementById('message');
      const formMessage = document.getElementById('form-message');

      let isValid = true;
      let errorMessage = '';

      // Reset form message state
      if (formMessage) {
        formMessage.style.display = 'none';
        formMessage.className = 'form-message';
        formMessage.textContent = '';
      }

      // Simple Validation Checks
      if (!nameInput || nameInput.value.trim() === '') {
        isValid = false;
        errorMessage = 'Zəhmət olmasa adınızı qeyd edin.';
      } else if (!emailInput || !validateEmail(emailInput.value.trim())) {
        isValid = false;
        errorMessage = 'Düzgün bir e-mail ünvanı daxil edin.';
      } else if (!messageInput || messageInput.value.trim() === '') {
        isValid = false;
        errorMessage = 'Zəhmət olmasa mesajınızı yazın.';
      }

      if (isValid) {
        // Mock successful form submission
        if (formMessage) {
          formMessage.classList.add('success');
          formMessage.textContent = 'Təşəkkür edirik! Mesajınız uğurla göndərildi. Sizinlə tezliklə əlaqə saxlayacağıq.';
          formMessage.style.display = 'block';
        }
        contactForm.reset();
      } else {
        // Show validation error
        if (formMessage) {
          formMessage.classList.add('error');
          formMessage.textContent = errorMessage;
          formMessage.style.display = 'block';
        }
      }
    });

    // Helper function for Email validation
    function validateEmail(email) {
      const re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
      return re.test(String(email).toLowerCase());
    }
  }
});
