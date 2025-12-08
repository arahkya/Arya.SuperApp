namespace Arya.SuperApp.Application.Interfaces;

public interface IResultOption<in TOption, out TResult>
{
    TResult Result { get; }
}