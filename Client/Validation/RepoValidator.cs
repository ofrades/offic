using System;
using FluentValidation; 

namespace Client.Validation {
    public class RepoValidator : AbstractValidator<Repo> {
        public RepoValidator(){
            RuleFor(v => v.owner).NotNull().WithMessage("Owner Required");
        }
    }
    public class UpdateContentValidator : AbstractValidator<UpdateContent> {
        public UpdateContentValidator(){
            RuleFor(v => v.commitMessage).NotNull().WithMessage("Commit Message Required");
            RuleFor(v => v.content).NotNull().WithMessage("New Content Required");
        }
    }
    public class NewContentValidator : AbstractValidator<NewContent> {
        public NewContentValidator(){
            RuleFor(v => v.commitMessage).NotNull().WithMessage("Commit Message Required");
            RuleFor(v => v.newContent).NotNull().WithMessage("New Content Required");
            RuleFor(v => v.newFileExtension).NotNull().WithMessage("File Extension Required");
            RuleFor(v => v.newFileName).NotNull().WithMessage("File Name Required");
        }
    }
}
