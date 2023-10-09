using Bogus;
using Letemknow.Shared;

namespace Letemknow.Server;

public interface ITestDataGenerator
{
    MailLink CreateMail();
}

internal sealed class TestDataGenerator : ITestDataGenerator
{
    public TestDataGenerator()
    {
        _faker = new Faker<MailLink>()
            .RuleFor(l=>l.Id, f=>f.Lorem.Slug())
            .RuleFor(l => l.Recipient, f => f.Internet.Email())
            .RuleFor(l => l.Subject, f => f.Rant.Review())
            .RuleFor(l => l.Body, f => f.Lorem.Paragraphs())
            .RuleFor(l => l.Title, f => f.Lorem.Sentence(2, 5))
            .RuleFor(l => l.Description, f => f.Lorem.Paragraph());
    }

    public MailLink CreateMail() => _faker;

    private Faker<MailLink> _faker;
}
